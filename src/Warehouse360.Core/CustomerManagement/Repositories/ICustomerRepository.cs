using Warehouse360.Core.CustomerManagement.Entities;

namespace Warehouse360.Core.CustomerManagement.Repositories;

public interface ICustomerRepository
{
    Task<Customer> GetCustomerByIdAsync(Guid customerId);
}