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

using Common.Constant;
using Common.Middleware;
using Timer.DAL.Timer.DAL.Repositories;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;
using ServiceTier.Services;
using Swashbuckle.AspNetCore.Swagger;
using gtdtimer.Services;
using gtdtimer.IoC;

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
                    builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
            });

            services.AddDbContext<TimerContext>(opts => opts.UseSqlServer(IoCContainer.Configuration["AzureConnection"]));
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<TimerContext>().AddDefaultTokenProviders();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<ILogInService, LogInService>();

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Common.Constant.Constants.SecretKey))
                };
            }
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}