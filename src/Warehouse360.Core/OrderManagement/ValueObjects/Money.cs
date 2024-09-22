using Warehouse360.Core.SeedWork.ValueObjects;

namespace Warehouse360.Core.OrderManagement.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        Amount = amount < 0
            ? throw new ArgumentException("Amount cannot be negative") 
            : amount;;
        
        Currency = string.IsNullOrWhiteSpace(currency)
            ? throw new ArgumentNullException(nameof(currency)) 
            : currency;
    }
    
    public Money ConvertTo(string targetCurrency, decimal exchangeRate)
    {
        var convertedAmount = Amount * exchangeRate;
        return new Money(convertedAmount, targetCurrency);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}