namespace Warehouse360.Core.OrderManagement.Events;

public class OrderItemRemoved
{
    public Guid OrderId { get; }
    public Guid ProductId { get; }

    public OrderItemRemoved(Guid orderId, Guid productId)
    {
        OrderId = orderId;
        ProductId = productId;
    }
}