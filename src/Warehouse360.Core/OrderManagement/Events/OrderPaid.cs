namespace Warehouse360.Core.OrderManagement.Events;

public class OrderPaid
{
    public Guid OrderId { get; }
    public DateTime PaidDate { get; }

    public OrderPaid(Guid orderId, DateTime paidDate)
    {
        OrderId = orderId;
        PaidDate = paidDate;
    }
}