namespace Challenge.Process.Aiq.Services.FavoriteProductServices;

public sealed record CreateFavoriteProductToCustomerDto
{
    public long CustomerId { get; set; }
    public long ProductId { get; set; }
}