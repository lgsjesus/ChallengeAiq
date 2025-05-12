using Challenge.Process.Aiq.Domain.Abstractions;
using Challenge.Process.Aiq.Domain.Entities;
using Challenge.Process.Aiq.Domain.Interfaces;
using Challenge.Process.Aiq.EntityFramework.Abstractions;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Challenge.Process.Aiq.EntityFramework.Repositories;

public sealed class CustomerRepository(ChallengeProcessAiqDbContext context, IUnitOfWork unitOfWork)
    : Repository<Customer>(context, unitOfWork), ICustomerRepository
{
    public async Task<Option<Customer>> GetCustomerByIdAsync(long customerId)
    {
        return await GetByIdAsync(customerId);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomerAsync(Pagination pagination)
    {
        return await GetAllAsync(pagination);
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        return await CreateAsync(customer);
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        return await UpdateAsync(customer);
    }
    public async Task<bool> ExistsCustomerByIdAsync(long customerId)
    {
        return await ExistsByIdAsync(customerId);
    }
    public async Task RemoveCustomerAsync(Customer customer)
    {
        await DeleteEntityAsync(customer);
    }
    public async Task<Option<Customer>> GetCustomerByEmailAsync(string email)
    {
        var customer = await DbSet.FirstOrDefaultAsync(c => string.Equals(c.Email.ToLower(), email.ToLower()));
        return customer == null ? Option.None<Customer>() : Option.Some(customer);
    }

}