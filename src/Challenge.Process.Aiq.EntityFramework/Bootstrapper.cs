using Challenge.Process.Aiq.Domain.Interfaces;
using Challenge.Process.Aiq.EntityFramework.Abstractions;
using Challenge.Process.Aiq.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Process.Aiq.EntityFramework;

public static class Bootstrapper
{
    public static void RegisterDbService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
        serviceCollection.AddScoped<IFavoriteProductRepository, FavoriteProductRepository>();
    }
}