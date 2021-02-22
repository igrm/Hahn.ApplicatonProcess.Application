using Hahn.ApplicatonProcess.February2021.Data.Infrastructure;
using Hahn.ApplicatonProcess.February2021.Data.Repositories;
using Hahn.ApplicatonProcess.February2021.Data.Services;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Models.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Hahn.ApplicatonProcess.February2021.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CommonProfile));
            services.AddMemoryCache();
            services.AddDbContext<AssetContext>(options=> {
                options.UseInMemoryDatabase(databaseName: Assembly.GetExecutingAssembly().GetName().Name);
            });

            services.AddHttpClient<ICountryRepository, CountryRepository>()
                    .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
                                                          .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                                                          .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
            
            services.AddHttpClient<ITopLevelDomainRepository, TopLevelDomainRepository>()
                    .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
                                                          .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                                                          .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
            
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IListOfValuesService, ListOfValuesService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IValidationService, ValidationService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn.ApplicatonProcess.February2021.Web", Version = "v1" });

                var domainFilePath = Path.Combine(AppContext.BaseDirectory, "Hahn.ApplicatonProcess.February2021.Domain.xml");
                if (File.Exists(domainFilePath)) c.IncludeXmlComments(domainFilePath);

                var webFilePath = Path.Combine(AppContext.BaseDirectory, "Hahn.ApplicatonProcess.February2021.Web.xml");
                if (File.Exists(webFilePath)) c.IncludeXmlComments(webFilePath);

            });

            services.AddControllers();

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {        
            Initialize(serviceProvider);
            
            app.UseSerilogRequestLogging();

            if (env.IsDevelopment())
            { 
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicatonProcess.February2021.Web v1"));
            

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void Initialize(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetService<ILogger<Startup>>();
            logger.LogInformation("Initializing...");

            var settingsService = serviceProvider.GetService<ISettingsService>();
            if (settingsService is not null)
            {
                var cultureInfo = settingsService.GetCurrentCulture();
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

                logger.LogInformation("Application culture set to {0}", cultureInfo);
            };

            var listOfValuesService = serviceProvider.GetService<IListOfValuesService>();
            if (listOfValuesService is not null)
            {
                listOfValuesService.PutAllCountriesInCache();
                listOfValuesService.PutAllAllTopLevelDomainsInCache();
            }
           
        }
    }
}
