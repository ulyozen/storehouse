using Warehouse360.Core.SeedWork.ValueObjects;

namespace Warehouse360.Core.OrderManagement.ValueObjects;

public class PaymentDetails : ValueObject
{
    public string CardNumber { get; }
    public string ExpirationDate { get; }
    public string CVC { get; }
    public decimal Amount { get; }
    public string Currency { get; }

    public PaymentDetails(string cardNumber, string expirationDate, string cvc, decimal amount, string currency)
    {
        CardNumber = cardNumber;
        ExpirationDate = expirationDate;
        CVC = cvc;
        Amount = amount;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CardNumber;
        yield return ExpirationDate;
        yield return CVC;
        yield return Amount;
        yield return Currency;
    }
}