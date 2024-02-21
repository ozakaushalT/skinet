using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(_ =>
            {
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT auth bearer scheme",
                    Name = "Authorisation",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                _.AddSecurityDefinition("Bearer", securitySchema);

                var secRequirement = new OpenApiSecurityRequirement
                {
                    {
                        securitySchema, new[]{ "Bearer"}
                    }
                };

                _.AddSecurityRequirement(secRequirement);
            });
            return services;
        }

        public static IApplicationBuilder UserSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}