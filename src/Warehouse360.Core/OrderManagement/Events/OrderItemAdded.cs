namespace Warehouse360.Core.OrderManagement.Events;

public class OrderItemAdded
{
    public Guid OrderId { get; }
    public Guid ProductId { get; }
    public int Quantity { get; }
    public decimal Price { get; }

    public OrderItemAdded(Guid orderId, Guid productId, int quantity, decimal price)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
}