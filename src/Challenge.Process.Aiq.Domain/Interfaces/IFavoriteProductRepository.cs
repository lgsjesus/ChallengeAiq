using Challenge.Process.Aiq.Domain.Entities;
using Optional;

namespace Challenge.Process.Aiq.Domain.Interfaces;

public interface IFavoriteProductRepository
{
    Task<Option<IEnumerable<FavoriteProduct>>> GetAllFavoriteProductsByCustomerIdAsync(long customerId);
    Task<FavoriteProduct> CreateFavoriteProductAsync(FavoriteProduct favoriteProduct);
    Task<Option<FavoriteProduct>> GetFavoriteProductAsync(long customerId, long productId);
    Task RemoveFavoriteProductAsync(FavoriteProduct favoriteProduct);
}