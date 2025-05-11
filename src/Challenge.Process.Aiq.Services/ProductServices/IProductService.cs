using Challenge.Process.Aiq.Domain.Entities;

namespace Challenge.Process.Aiq.Services.ProductServices;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
    Task<ProductDto> GetProductById(long productId);

    /// <summary>
    /// That method will be query on database if product exists. If found product returns it, else will create it and return.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task<Product> CreateOrGetProductAsync(Product product);
}