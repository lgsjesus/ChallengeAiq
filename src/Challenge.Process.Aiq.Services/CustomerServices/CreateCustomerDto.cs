using System.ComponentModel.DataAnnotations;
using Challenge.Process.Aiq.Domain.Entities;

namespace Challenge.Process.Aiq.Services.CustomerServices;

public sealed record CreateCustomerDto
{
    [MaxLength(120)]
    public required string Name { get; init; }
    [EmailAddress]
    [MaxLength(200)]
    public required string Email { get; init; }
    
    public static implicit operator Customer(CreateCustomerDto customer) => Customer.Create(customer.Name,customer.Email);
    
}