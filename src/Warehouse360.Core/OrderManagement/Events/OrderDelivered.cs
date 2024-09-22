namespace Warehouse360.Core.OrderManagement.Events;

public class OrderDelivered
{
    public Guid OrderId { get; }
    public DateTime DeliveredDate { get; }

    public OrderDelivered(Guid orderId, DateTime deliveredDate)
    {
        OrderId = orderId;
        DeliveredDate = deliveredDate;
    }
}