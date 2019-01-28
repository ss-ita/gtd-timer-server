//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

using GtdCommon.IoC;
using GtdTimer.Middleware;
using GtdServiceTier.Services;
using GtdTimerDAL.Entities;
using GtdTimerDAL.Repositories;
using GtdTimerDAL.UnitOfWork;

namespace GtdTimer
{
    /// <summary>
    /// class for configuring project
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="env">hosting environment</param>
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
                config["AzureKeyVault:clientSecret"]);
            IoCContainer.Configuration = builder.Build();
        }

        /// <summary>
        /// Method for configuring services
        /// </summary>
        /// <param name="services">list of services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowSpecificOrigin",
                    builder => builder.WithOrigins(IoCContainer.Configuration["Origins"]).AllowAnyHeader().AllowAnyMethod());
            });
            services.AddDbContext<TimerContext>(opts => opts.UseSqlServer(IoCContainer.Configuration["AzureConnection"]));
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<TimerContext>().AddDefaultTokenProviders();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILogInService, LogInService>();
            services.AddScoped<IPresetService, PresetService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IAlarmService, AlarmService>();
            services.AddScoped<IUserIdentityService, UserIdentityService>();

            services.AddScoped<IRepository<PresetTasks>, Repository<PresetTasks>>();
            services.AddScoped<IRepository<Preset>, Repository<Preset>>();
            services.AddScoped<IRepository<Role>, Repository<Role>>();
            services.AddScoped<IRepository<Tasks>, Repository<Tasks>>();
            services.AddScoped<IRepository<UserRole>, Repository<UserRole>>();
            services.AddScoped<IRepository<Record>, Repository<Record>>();
            services.AddScoped<IRepository<Alarm>, Repository<Alarm>>();
            services.AddScoped<IApplicationUserManager<User, int>, ApplicationUserManager>();
            services.AddScoped<IUserStore<User, int>, UserRepository>();

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
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    IoCContainer.Configuration.GetValue<string>("SwaggerDocument:Name"),
                    new Info
                    {
                        Title = IoCContainer.Configuration.GetValue<string>("SwaggerDocument:Title"),
                        Version = IoCContainer.Configuration.GetValue<string>("SwaggerDocument:Version")
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        /// <summary>
        /// Method for configuring application
        /// </summary>
        /// <param name="app">application builder</param>
        /// <param name="env">hosting environment</param>
        /// <param name="loggerFactory">class which registers logger</param>
        /// <param name="configuration">class which helps configure project</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IConfiguration configuration)
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
                    IoCContainer.Configuration.GetValue<string>("SwaggerDocument:Name"));
            });

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            loggerFactory.AddLog4Net(configuration.GetValue<string>("Log4NetConfigFile:Name"));
            app.UseMvc();
        }
    }
}