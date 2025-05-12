using Challenge.Process.Aiq.Domain.Entities;
using Challenge.Process.Aiq.Services.ProductServices;

namespace Challenge.Process.Aiq.Services.FavoriteProductServices;

public record FavoriteProductsDto
{
    public long CustomerId { get; private set; }
    public  string CustomerName { get;  private set; } =string.Empty;
    public string CustomerEmail { get;  private set; } =string.Empty;
    public IEnumerable<ProductDto> FavoriteProducts { get;  private set; } = [];

    public static FavoriteProductsDto Create(in long customerId, in string customerName, in string customerEmail,
        IList<Product> favoriteProducts) => new()
    {
        CustomerId = customerId,
        CustomerName = customerName,
        CustomerEmail = customerEmail,
        FavoriteProducts = favoriteProducts.Select(c => (ProductDto) c).ToList()
    };
}