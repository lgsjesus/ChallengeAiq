using System.Collections.ObjectModel;
using Challenge.Process.Aiq.Domain.Abstractions;
using Challenge.Process.Aiq.Domain.Entities;
using Optional;

namespace Challenge.Process.Aiq.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<Option<Customer>> GetCustomerByIdAsync(long customerId);
    Task<bool> ExistsCustomerByIdAsync(long customerId);
    Task<IEnumerable<Customer>> GetAllCustomerAsync(Pagination pagination);
    Task<Customer> CreateCustomerAsync(Customer customer);
    Task<Customer> UpdateCustomerAsync(Customer customer);
    Task RemoveCustomerAsync(Customer customer);
    Task<Option<Customer>> GetCustomerByEmailAsync(string email);
}