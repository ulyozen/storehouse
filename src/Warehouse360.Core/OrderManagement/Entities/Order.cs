using Warehouse360.Core.OrderManagement.Enums;
using Warehouse360.Core.OrderManagement.Events;
using Warehouse360.Core.OrderManagement.ValueObjects;
using Warehouse360.Core.SeedWork.Entities;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Core.OrderManagement.Entities;

public class Order : BaseEntity, IAggregateRoot
{
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public OrderStatus Status { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    
    private readonly List<object> _domainEvents = new();

    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    public Order(Guid customerId)
    {
        CustomerId = customerId;
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.Pending;
        Items = new List<OrderItem>();
        PaymentStatus = PaymentStatus.Pending;
    }
    
    public void AddOrderItem(Guid productId, int quantity, Money price)
    {
        if (Items.Any(i => i.ProductId == productId))
            throw new InvalidOperationException("Product already added to the order.");

        var orderItem = new OrderItem(productId, quantity, price);
        Items.Add(orderItem);
        
        _domainEvents.Add(new OrderItemAdded(Id, productId, quantity, price.Amount));
    }
    
    public void UpdateOrderItem(Guid productId, int newQuantity, Money newPrice)
    {
        var orderItem = Items.SingleOrDefault(i => i.ProductId == productId);
        if (orderItem == null)
            throw new InvalidOperationException("Product not found in the order.");

        orderItem.UpdateQuantity(newQuantity);
        orderItem.UpdatePrice(newPrice);

        _domainEvents.Add(new OrderItemUpdated(Id, productId, newQuantity, newPrice.Amount));
    }
    
    public void RemoveOrderItem(Guid productId)
    {
        var orderItem = Items.SingleOrDefault(i => i.ProductId == productId);
        if (orderItem == null)
            throw new InvalidOperationException("Product not found in the order.");

        Items.Remove(orderItem);
        
        _domainEvents.Add(new OrderItemRemoved(Id, productId));
    }
    
    public decimal CalculateTotalPrice()
    {
        return Items.Sum(item => item.CalculateTotalPrice());
    }
    
    public Money CalculateTotalPriceInCurrency(string targetCurrency, decimal exchangeRate)
    {
        var totalAmountInCurrency = Items
            .Select(item => item.GetTotalPriceInCurrency(targetCurrency, exchangeRate))
            .Sum(money => money.Amount);

        return new Money(totalAmountInCurrency, targetCurrency);
    }
    
    public void MarkAsPaid()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Order must be pending to mark as paid.");

        Status = OrderStatus.Paid;
        _domainEvents.Add(new OrderPaid(Id, DateTime.UtcNow));
    }
    
    public void MarkAsShipped()
    {
        if (Status != OrderStatus.Paid)
            throw new InvalidOperationException("Order must be paid before it can be shipped.");

        Status = OrderStatus.Shipped;
        _domainEvents.Add(new OrderShipped(Id, DateTime.UtcNow));
    }
    
    public void MarkAsDelivered()
    {
        if (Status != OrderStatus.Shipped)
            throw new InvalidOperationException("Order must be shipped before it can be delivered.");

        Status = OrderStatus.Delivered;
        _domainEvents.Add(new OrderDelivered(Id, DateTime.UtcNow));
    }
    
    public void CancelOrder()
    {
        if (Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Cannot cancel an order that has been delivered.");

        Status = OrderStatus.Cancelled;
        _domainEvents.Add(new OrderCancelled(Id));
    }
}