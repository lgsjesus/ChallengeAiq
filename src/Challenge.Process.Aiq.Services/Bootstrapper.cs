using Challenge.Process.Aiq.Domain.Abstractions;
using Challenge.Process.Aiq.Services.CustomerServices;
using Challenge.Process.Aiq.Services.FavoriteProductServices;
using Challenge.Process.Aiq.Services.ProductServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Process.Aiq.Services;

public static class Bootstrapper
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IFavoriteProductService, FavoriteProductService>();
        services.AddHttpClient<IProductService, ProductService>(
            c=> c.BaseAddress = new(configuration["ServicesAgents:ProductService"] ?? 
                                    throw new UserException("Without configuration to ProductService in appSettings")));
    }
}