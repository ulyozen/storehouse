namespace Warehouse360.Core.OrderManagement.Events;

public class OrderCancelled
{
    public Guid OrderId { get; }

    public OrderCancelled(Guid orderId)
    {
        OrderId = orderId;
    }
}