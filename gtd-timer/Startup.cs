using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Reflection;
using System.Text;

using gtdtimer.Middleware;
using Timer.DAL.Timer.DAL.Repositories;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;
using ServiceTier.Services;
using Swashbuckle.AspNetCore.Swagger;
using gtdtimer.Services;
using Common.IoC;

namespace gtd_timer
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                 .AddJsonFile("azurekeyvault.json", optional: false, reloadOnChange: true)
                 .AddEnvironmentVariables();

            var config = builder.Build();
            

            builder.AddAzureKeyVault(
                $"https://{config["AzureKeyVault:vault"]}.vault.azure.net/",
                config["AzureKeyVault:clientId"],
                config["AzureKeyVault:clientSecret"]
            );
            IoCContainer.Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins(IoCContainer.Configuration["Origins"]).AllowAnyHeader().AllowAnyMethod());
            });
          
            services.AddDbContext<TimerContext>(opts => opts.UseSqlServer(IoCContainer.Configuration["AzureConnection"]));
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<TimerContext>().AddDefaultTokenProviders();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILogInService, LogInService>();
            services.AddScoped<IPresetService, PresetService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IUserIdentityService, UserIdentityService>();

            services.AddScoped<IRepository<Timer.DAL.Timer.DAL.Entities.Timer>, Repository<Timer.DAL.Timer.DAL.Entities.Timer>>();
            services.AddScoped<IRepository<Preset>, Repository<Preset>>();
            services.AddScoped<IRepository<Role>, Repository<Role>>();
            services.AddScoped<IApplicationUserManager<User, int>, ApplicationUserManager>();
            services.AddScoped<IUserStore<User,int>, UserRepository>(); 

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opts =>
            {
                opts.SaveToken = true;
                opts.RequireHttpsMetadata = false;
                opts.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Tokens:Issuer",
                    ValidAudience = "Tokens:Audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IoCContainer.Configuration["JWTSecretKey"]))
                };
            }
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    IoCContainer.Configuration.GetValue<string>("SwaggerDocument:Name"),
                    new Info {
                        Title = IoCContainer.Configuration.GetValue<string>("SwaggerDocument:Title"),
                        Version =  IoCContainer.Configuration.GetValue<string>("SwaggerDocument:Version")
                        });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowSpecificOrigin");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    $"/swagger/{IoCContainer.Configuration.GetValue<string>("SwaggerDocument:Name")}/swagger.json",
                    IoCContainer.Configuration.GetValue<string>("SwaggerDocument:Name")
                    );
            });
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}