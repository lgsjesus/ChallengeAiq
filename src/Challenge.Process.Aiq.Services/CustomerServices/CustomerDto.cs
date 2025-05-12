using System.ComponentModel.DataAnnotations;
using Challenge.Process.Aiq.Domain.Entities;

namespace Challenge.Process.Aiq.Services.CustomerServices;

public sealed record CustomerDto
{
    public long Id { get; init; }
    [MaxLength(120)]
    [MinLength(5)]
    [Required]
    public required string Name { get; init; }
    [EmailAddress]
    [MaxLength(200)]
    public required string Email { get; init; }

    public static implicit operator CustomerDto(Customer customer) => new()
    {
        Id = customer.Id,
        Name = customer.Name,
        Email = customer.Email
    };
    
    public static implicit operator Customer(CustomerDto customer) => Customer.Create(customer.Name,customer.Email);

    public static List<CustomerDto> ToList(IEnumerable<Customer> customers)
    {
        var customerDtos = new List<CustomerDto>();
        foreach (Customer customer in customers)
        {
            customerDtos.Add(customer);
        }

        return customerDtos;
    }
}