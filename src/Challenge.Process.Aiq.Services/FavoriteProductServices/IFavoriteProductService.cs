namespace Challenge.Process.Aiq.Services.FavoriteProductServices;

public interface IFavoriteProductService
{
    Task CreateFavoriteProductToCustomerAsync(CreateFavoriteProductToCustomerDto createFavoriteProductToCustomerDto);
    Task<FavoriteProductsDto> GetAllFavoriteProductsByCustomerIdAsync(long customerId);
    Task RemoveFavoriteProductFromCustomer(RemoveFavoriteProductToCustomerDto removeFavoriteProductToCustomerDto);
}