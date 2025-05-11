using Challenge.Process.Aiq.Domain.Abstractions;

namespace Challenge.Process.Aiq.Domain.Entities;

public sealed class FavoriteProduct : Entity
{
    public long ProductId { get; private set; }
    public Product Product { get; init; } = null!;
    public long CustomerId { get; private set; }
    public Customer Customer { get; init; }  = null!;
    
    public static FavoriteProduct Create(in long productId, in long customerId) => new()
    {
        ProductId = productId,
        CustomerId = customerId,
    };
}