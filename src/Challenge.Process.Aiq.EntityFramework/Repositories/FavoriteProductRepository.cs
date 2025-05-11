using Challenge.Process.Aiq.Domain.Abstractions;
using Challenge.Process.Aiq.Domain.Entities;
using Challenge.Process.Aiq.Domain.Interfaces;
using Challenge.Process.Aiq.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Challenge.Process.Aiq.EntityFramework.Repositories;

public sealed class FavoriteProductRepository(ChallengeProcessAiqDbContext context,IUnitOfWork unitOfWork)
    : Repository<FavoriteProduct>(context,unitOfWork), IFavoriteProductRepository
{
    public async Task<Option<IEnumerable<FavoriteProduct>>> GetAllFavoriteProductsByCustomerIdAsync(long customerId)
    {
        var favorites = await DbSet.Include(f=>f.Product)
            .Where(f => f.CustomerId == customerId).ToListAsync();
        return favorites.Count > 0 ? Option.Some<IEnumerable<FavoriteProduct>>(favorites) : Option.None<IEnumerable<FavoriteProduct>>();
    }
    public async Task<FavoriteProduct> CreateFavoriteProductAsync(FavoriteProduct favoriteProduct)
    {
        return await CreateAsync(favoriteProduct);
    }
    public async Task<Option<FavoriteProduct>> GetFavoriteProductAsync(long customerId, long productId)
    {
       var favoriteProduct = await DbSet.FirstOrDefaultAsync(f => f.CustomerId == customerId && f.ProductId == productId);
       return favoriteProduct != null ? Option.Some<FavoriteProduct>(favoriteProduct) : Option.None<FavoriteProduct>();
    }

    public async Task RemoveFavoriteProductAsync(FavoriteProduct favoriteProduct)
    {
        await DeleteEntityAsync(favoriteProduct);
    }
}