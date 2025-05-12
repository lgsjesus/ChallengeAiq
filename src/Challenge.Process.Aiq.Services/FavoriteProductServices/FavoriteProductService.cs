using Challenge.Process.Aiq.Domain.Abstractions;
using Challenge.Process.Aiq.Domain.Entities;
using Challenge.Process.Aiq.Domain.Interfaces;
using Challenge.Process.Aiq.Services.CustomerServices;
using Challenge.Process.Aiq.Services.ProductServices;
using Optional.Unsafe;

namespace Challenge.Process.Aiq.Services.FavoriteProductServices;

public sealed class FavoriteProductService(
    IFavoriteProductRepository favoriteProductRepository,
    ICustomerRepository customerRepository,
    IProductService productService,
    ICustomerService customerService)
    : IFavoriteProductService
{
    public async Task CreateFavoriteProductToCustomerAsync(
        CreateFavoriteProductToCustomerDto createFavoriteProductToCustomerDto)
    {
        var alreadyExistFavoriteToCustomer =
            await favoriteProductRepository.GetFavoriteProductAsync(createFavoriteProductToCustomerDto.CustomerId,
                createFavoriteProductToCustomerDto.ProductId);

        if (alreadyExistFavoriteToCustomer.HasValue)
            throw new UserException("Favorite product already exists to customer");

        if (!await customerService.ExistsCustomerByIdAsync(createFavoriteProductToCustomerDto.CustomerId))
            throw new UserNotFoundException("Not found Customer by Id");

        var productDto = await productService.GetProductById(createFavoriteProductToCustomerDto.ProductId);

        var product = await productService.CreateOrGetProductAsync(
            Product.Create(productDto.Id,
                productDto.Title,
                productDto.Image,
                productDto.Description,
                (decimal) productDto.Price));

        await favoriteProductRepository.CreateFavoriteProductAsync(FavoriteProduct.Create(product.Id,
            createFavoriteProductToCustomerDto.CustomerId));
    }

    public async Task<FavoriteProductsDto> GetAllFavoriteProductsByCustomerIdAsync(long customerId)
    {
        var favoriteProductOption = await customerRepository.GetCustomerByIdAsync(customerId);
        if (!favoriteProductOption.HasValue)
            throw new UserNotFoundException("Customer not found");

        return (await favoriteProductRepository.GetAllFavoriteProductsByCustomerIdAsync(customerId))
            .Match(some: favoriteProducts =>
                {
                    var customer = favoriteProductOption.ValueOrDefault();
                    return FavoriteProductsDto.Create(customer.Id, customer.Name, customer.Email,
                        favoriteProducts.Select(f => f.Product).ToList());
                },
                none: () => throw new UserNotFoundException("There are not favorites products for customer"));
    }
    public async Task RemoveFavoriteProductFromCustomer(RemoveFavoriteProductToCustomerDto removeFavoriteProductToCustomerDto)
    {
        var favoriteProductOption = await favoriteProductRepository.GetFavoriteProductAsync(removeFavoriteProductToCustomerDto.CustomerId,
                removeFavoriteProductToCustomerDto.ProductId);

        if (!favoriteProductOption.HasValue)
            throw new UserNotFoundException("Favorite product not exists to customer");

        await favoriteProductRepository.RemoveFavoriteProductAsync(favoriteProductOption.ValueOrDefault());
    }
}