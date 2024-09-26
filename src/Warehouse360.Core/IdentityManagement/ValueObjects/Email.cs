using Warehouse360.Core.SeedWork.ValueObjects;

namespace Warehouse360.Core.IdentityManagement.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; private set; }

    public Email(string value)
    {
        if (string.IsNullOrEmpty(value) || !value.Contains("@"))
        {
            throw new ArgumentException("Invalid email format");
        }
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}