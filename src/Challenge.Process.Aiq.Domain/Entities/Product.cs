using System.ComponentModel.DataAnnotations;
using Challenge.Process.Aiq.Domain.Abstractions;

namespace Challenge.Process.Aiq.Domain.Entities;

public sealed class Product :Entity
{
    [MaxLength(300)]
    public string Title { get; private set; } =string.Empty;
    public string? Image { get; private set; }
    [MaxLength(3000)]
    public string? Review { get;private set; }
    public decimal Price { get; private set; }

    public static Product Create(in long id, in string title,in string image,in string review,in decimal price) => new()
    {
        Id = id,
        Title = title,
        Image = image,
        Review = review,
        Price = price,
    };
    public void Update (in string title,in string image, in string review, in decimal price)
    {
        Title = title;
        Image = image;
        Review = review;
        Price = price;
    }
}