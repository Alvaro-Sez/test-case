using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ClayLocks.Configuration;

public static class SwaggerConfiguration
{
  public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("oauth2",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                opt.OperationFilter<SecurityRequirementsOperationFilter>();
            });
    }}