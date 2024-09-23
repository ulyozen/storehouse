using Warehouse360.Core.CustomerManagement.ValueObjects;
using Warehouse360.Core.SeedWork.Entities;

namespace Warehouse360.Core.CustomerManagement.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public ContactInformation ContactInfo { get; private set; }

    public Customer(string firstName, string lastName, ContactInformation contactInfo)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        ContactInfo = contactInfo ?? throw new ArgumentNullException(nameof(contactInfo));
    }

    public void UpdateContactInformation(ContactInformation newContactInfo)
    {
        if (newContactInfo == null)
            throw new ArgumentNullException(nameof(newContactInfo));

        ContactInfo = newContactInfo;
    }
}