using Challenge.Process.Aiq.Domain.Entities;
using Optional;

namespace Challenge.Process.Aiq.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task<Option<Product>> GetProductById(long productId);
}