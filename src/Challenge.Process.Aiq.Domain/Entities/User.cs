using System.ComponentModel.DataAnnotations;
using Challenge.Process.Aiq.Domain.Abstractions;

namespace Challenge.Process.Aiq.Domain.Entities;

public class User : Entity
{
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }

    public static User Create(in string email, in string password) => new()
    {
        Email = email,
        Password = password,
    };
}