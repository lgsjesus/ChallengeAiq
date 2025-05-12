using Challenge.Process.Aiq.Domain.Entities;
using Challenge.Process.Aiq.Domain.Interfaces;
using Challenge.Process.Aiq.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Challenge.Process.Aiq.EntityFramework.Repositories;

public sealed class ProductRepository(ChallengeProcessAiqDbContext context,IUnitOfWork unitOfWork)
    : Repository<Product>(context,unitOfWork), IProductRepository
{
    public async Task<Product> CreateProductAsync(Product product)
    {
        return await CreateAsync(product);
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        return await UpdateAsync(product);
    }

    public async Task<Option<Product>> GetProductById(long productId)
    {
        var product = await DbSet.FirstOrDefaultAsync(p=> p.Id == productId);
        return product == null ? Option.None<Product>() : Option.Some(product);
    }
}