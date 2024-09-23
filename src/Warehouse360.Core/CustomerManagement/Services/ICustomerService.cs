using Warehouse360.Core.CustomerManagement.ValueObjects;

namespace Warehouse360.Core.CustomerManagement.Services;

public interface ICustomerService
{
    Task<bool> UpdateCustomerContactInfo(Guid customerId, ContactInformation newContactInfo);
}

/*
 
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task UpdateCustomerContactInfo(Guid customerId, ContactInformation newContactInfo)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer == null)
        {
            throw new Exception("Customer not found");
        }

        customer.UpdateContactInformation(newContactInfo);
        await _unitOfWork.CommitAsync();
    }
}

*/