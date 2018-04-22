using System;
using System.Net;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SoftwareHouse.Web.Identity.Services;
using Microsoft.AspNetCore.SpaServices.Webpack;
using SoftwareHouse.DataAccess;
using SoftwareHouse.DataAccess.Models;
using SoftwareHouse.DataAccess.Repositories;
using SoftwareHouse.Services.Services;
using SoftwareHouse.Contract.Interfaces;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using SoftwareHouse.Contract.Repositories;
using SoftwareHouse.Contract.Services;
using SoftwareHouse.Services.Services.UsersInformation;
using Swashbuckle.AspNetCore.Swagger;

namespace SoftwareHouse.Web
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "831154095231-sjqgq2pi9c49o3j9rd92542qkno7bi6i.apps.googleusercontent.com";
                googleOptions.ClientSecret = "_gifLvxKma3G820SCO9XAA5r";
            });


            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

         

       

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            // Repositories
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IPersonManagementRepository, PersonManagementRepository>();
            services.AddScoped<IPersonManagementService, PersonManagementService>();

            services.AddScoped<IExperiancesRepository, ExperiancesRepository>();
            services.AddScoped<IExperiancesService, ExperiancesService>();

            services.AddScoped<IQualificationsService, QualificationsService>();
            services.AddScoped<IQualificationsRepository, QualificationsRepository>();

            services.AddScoped<IUserRatingsRepository, UserRatingsRepository>();
            services.AddScoped<IUserRatingsService, UserRatingsService>();

            services.AddScoped<IPhotosOfWorkRepository, PhotosOfWorkRespository>();
            services.AddScoped<IPhotosOfWorkService, PhotosOfWorkService>();

            // Services
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // uri helper will use this
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>(); 
            services.AddScoped<IUrlHelper, UrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory
                    .GetService<IActionContextAccessor>()
                    .ActionContext;

                return new UrlHelper(actionContext);
            }); // instance created ones per request
        }
      

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true,
                    HotModuleReplacementEndpoint = "/dist/__webpack_hmr"
                });

                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor,

                    // IIS is also tagging a X-Forwarded-For header on, so we need to increase this limit, 
                    // otherwise the X-Forwarded-For we are passing along from the browser will be ignored
                    ForwardLimit = 2
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //Microsoft.AspNetCore.Builder.SwaggerBuilderExtensions
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "text/html";

                            var error = context.Features.Get<IExceptionHandlerFeature>();
                            if (error != null)
                            {
                                await context.Response.WriteAsync($"<h1>Error: {error.Error.Message}</h1>").ConfigureAwait(false);
                            }
                        });
                });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.  
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseAuthentication();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
