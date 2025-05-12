using System.Text.Json.Serialization;
using Challenge.Process.Aiq.Domain.Entities;

namespace Challenge.Process.Aiq.Services.ProductServices;

public sealed record ProductDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("title")]
    public required string Title { get; init; }

    [JsonPropertyName("price")]
    public double Price { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }  =string.Empty;
    
    [JsonPropertyName("image")]
    public string Image { get; init; } =string.Empty;

    public static ProductDto Create(in long id, in string title,in string image,in string review,in decimal price) => new()
    {
        Id = id,
        Title = title,
        Image = image,
        Description = review,
        Price =(double) price,
    };
    
    public static implicit operator Product(ProductDto productDto) =>
        Product.Create(productDto.Id,productDto.Title, productDto.Image , productDto.Description  , (decimal)productDto.Price);
    
    public static implicit operator ProductDto(Product productDto) =>
        Create(productDto.Id,productDto.Title, productDto.Image ?? string.Empty, productDto.Review ?? string.Empty, productDto.Price);
}