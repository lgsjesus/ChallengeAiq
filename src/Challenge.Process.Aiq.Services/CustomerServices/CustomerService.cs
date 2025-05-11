using Challenge.Process.Aiq.Domain.Abstractions;
using Challenge.Process.Aiq.Domain.Entities;
using Challenge.Process.Aiq.Domain.Interfaces;
using Optional.Unsafe;

namespace Challenge.Process.Aiq.Services.CustomerServices;

public sealed class CustomerService(ICustomerRepository repository) : ICustomerService
{
    public async Task<CustomerDto> GetByIdAsync(long id)
    {
        return (await repository.GetCustomerByIdAsync(id))
            .Match(some: customer => customer, none: () =>
                throw new UserNotFoundException("Customer not found."));
    }

    public async Task<CustomerDto> CreateAsync(CreateCustomerDto customerDto)
    {
        if ((await repository.GetCustomerByEmailAsync(customerDto.Email)).HasValue)
            throw new UserException("Email already exists");
        return await repository.CreateCustomerAsync(customerDto);
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync(Pagination pagination)
     => CustomerDto.ToList(await repository.GetAllCustomerAsync(pagination));

    public async Task<CustomerDto> UpdateAsync(CustomerDto customerDto)
    {
        return await (await repository.GetCustomerByIdAsync(customerDto.Id))
            .Match(some: async customer =>
            {
                if (await ValidationEmail(customer, customerDto.Email))
                {
                    customer.Update(customerDto.Name,customer.Email);
                    return await repository.UpdateCustomerAsync(customer);
                }

                throw new UserException("Email already exists in another account.");
            }, none: () => throw new UserNotFoundException("Customer not found."));
    }

    public async Task RemoveByIdAsync(long id)
    {
        await (await repository.GetCustomerByIdAsync(id))
            .Match(some: async customer =>
            {
                await repository.RemoveCustomerAsync(customer);

            }, none: () => throw new UserNotFoundException("Customer not found."));
    }
    public async Task<bool> ExistsCustomerByIdAsync(long customerId)
    {
        return await repository.ExistsCustomerByIdAsync(customerId);
    }

    private async Task<bool> ValidationEmail(Customer customer, string email)
    {
        var customerOption = await repository.GetCustomerByEmailAsync(email);
        return !customerOption.HasValue ||
               customerOption.ValueOrFailure().Id == customer.Id;
    }
}