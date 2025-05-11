using System.ComponentModel.DataAnnotations;
using Challenge.Process.Aiq.Domain.Abstractions;

namespace Challenge.Process.Aiq.Domain.Entities;

public sealed class Customer : Entity
{
    [MaxLength(120)]
    public string Name { get; private set; } = string.Empty;
    [MaxLength(200)]
    public string Email { get; private set; } = string.Empty;

    public IList<FavoriteProduct> FavoriteProducts { get; private set; } = new List<FavoriteProduct>();

    public static Customer Create(in string name, in string email) => new()
    {
        Name = name,
        Email = email,
    };
    public void Update(in string name, in string email)
    {
        Name = name;
        Email = email;
    }
}