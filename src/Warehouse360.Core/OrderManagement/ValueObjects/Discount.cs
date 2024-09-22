using Warehouse360.Core.OrderManagement.Enums;
using Warehouse360.Core.SeedWork.ValueObjects;

namespace Warehouse360.Core.OrderManagement.ValueObjects;

public class Discount : ValueObject
{
    public decimal Amount { get; }
    public DiscountType Type { get; }

    public Discount(decimal amount, DiscountType type)
    {
        Amount = amount > 0 ? amount : throw new ArgumentException("Amount must be greater than zero.");
        Type = type;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Type;
    }
}