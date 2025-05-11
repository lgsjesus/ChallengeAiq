using System.Collections.ObjectModel;
using Challenge.Process.Aiq.Domain.Abstractions;

namespace Challenge.Process.Aiq.Services.CustomerServices;

public interface ICustomerService
{
    Task<CustomerDto> GetByIdAsync(long id);
    Task<CustomerDto> CreateAsync(CreateCustomerDto customerDto);
    Task<IEnumerable<CustomerDto>> GetAllAsync(Pagination pagination);
    Task<CustomerDto> UpdateAsync(CustomerDto customerDto);
    Task RemoveByIdAsync(long id);
    Task<bool> ExistsCustomerByIdAsync(long customerId);
}