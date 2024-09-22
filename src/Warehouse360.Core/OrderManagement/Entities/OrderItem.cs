using Warehouse360.Core.OrderManagement.ValueObjects;
using Warehouse360.Core.SeedWork.Entities;

namespace Warehouse360.Core.OrderManagement.Entities;

public class OrderItem : BaseEntity
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }

    public OrderItem(Guid productId, int quantity, Money price)
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
    
    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");
            
        Quantity = newQuantity;
    }

    public void UpdatePrice(Money newPrice)
    {
        Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
    }

    public decimal CalculateTotalPrice()
    {
        return Price.Amount * Quantity;
    }
    
    public Money GetTotalPriceInCurrency(string targetCurrency, decimal exchangeRate)
    {
        var totalPrice = new Money(CalculateTotalPrice(), Price.Currency);
        return totalPrice.ConvertTo(targetCurrency, exchangeRate);
    }
}