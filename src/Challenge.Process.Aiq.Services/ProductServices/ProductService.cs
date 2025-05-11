using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using Challenge.Process.Aiq.Domain.Abstractions;
using Challenge.Process.Aiq.Domain.Entities;
using Challenge.Process.Aiq.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Optional.Unsafe;

namespace Challenge.Process.Aiq.Services.ProductServices;

public sealed class ProductService(
    HttpClient client,
    ILogger<ProductService> logger,
    IProductRepository productRepository) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        return await GetResult<IEnumerable<ProductDto>>(await client.GetAsync("products"));
    }

    public async Task<ProductDto> GetProductById(long productId)
    {
        return await GetResult<ProductDto>(await client.GetAsync($"products/{productId}"));
    }

    public async Task<Product> CreateOrGetProductAsync(Product product)
    {
        var productOption = await productRepository.GetProductById(product.Id);
        if (productOption.HasValue)
            return productOption.ValueOrDefault();
        return await productRepository.CreateProductAsync(product);
    }

    private async Task<T> GetResult<T>([NotNull] HttpResponseMessage response) where T : class
    {
        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            var stringContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(stringContent))
                throw new UserNotFoundException("Not found Product");

            return (await response.Content.ReadFromJsonAsync<T>()!)!;
        }

        logger.LogError("Get Products failed with status code {StatusCode}", response.StatusCode);
        throw new UserException($"Get Products failed with status code {response.StatusCode}");
    }
}