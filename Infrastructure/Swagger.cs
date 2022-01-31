using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;

namespace Infrastructure.ApiDocumentation
{
    public static class Swagger
    {
        public static void AddSwaggerFramework(this IServiceCollection services, IWebHostEnvironment environment)
        {
            //var env = string.IsNullOrWhiteSpace(environment.EnvironmentName) ? "Local" : environment.EnvironmentName;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "GitHubAPi Teste Fernando",
                        Version = "v1",
                        Description = $"Simple API to get my job .Net Core 3.1"
                    });

            });
        }

        public static void UseSwaggerFramework(this IApplicationBuilder application)
        {
            application.UseSwagger();
            application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation");
                c.DocumentTitle = "GitHubAPi Teste Fernando";
                c.DocExpansion(DocExpansion.List);
            });
        }
    }
}

