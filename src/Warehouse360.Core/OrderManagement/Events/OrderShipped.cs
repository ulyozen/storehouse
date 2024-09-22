namespace Warehouse360.Core.OrderManagement.Events;

public class OrderShipped
{
    public Guid OrderId { get; }
    public DateTime ShippedDate { get; }

    public OrderShipped(Guid orderId, DateTime shippedDate)
    {
        OrderId = orderId;
        ShippedDate = shippedDate;
    }
}