using Warehouse360.Core.SeedWork.ValueObjects;

namespace Warehouse360.Core.CustomerManagement.ValueObjects;

public class ContactInformation : ValueObject
{
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }

    public ContactInformation(string email, string phoneNumber)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email;
        yield return PhoneNumber;
    }
}