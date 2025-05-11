namespace Challenge.Process.Aiq.Services.FavoriteProductServices;

public sealed record RemoveFavoriteProductToCustomerDto
{
    public long CustomerId { get; set; }
    public long ProductId { get; set; }
}