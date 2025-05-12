using Challenge.Process.Aiq.Domain.Abstractions;
using Challenge.Process.Aiq.Services.TokenServices;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Challenge.Process.Aiq.WebApi.Abstractions.SwaggerExamples;

internal static class Examples
{
    public static void AddSwaggerExamples(this SwaggerGenOptions options)
    {
        options.MapType<Pagination>(() => new OpenApiSchema()
        {
            Example = new OpenApiObject
            {
                [nameof(Pagination.OrderBy)] = new OpenApiString("Id"),
                [nameof(Pagination.PageNumber)] = new OpenApiInteger(1),
                [nameof(Pagination.PageSize)] = new OpenApiInteger(10)
            }
        });
        
        options.MapType<AuthorizationRequestDto>(() => new OpenApiSchema()
        {
            Example = new OpenApiObject
            {
                [nameof(AuthorizationRequestDto.EmailUser)] = new OpenApiString("user@example.com"),
                [nameof(AuthorizationRequestDto.Password)] = new OpenApiString("admin1234")
            }
        });
    }
}