namespace Warehouse360.Core.OrderManagement.Events;

public class OrderItemUpdated
{
    public Guid OrderId { get; }
    public Guid ProductId { get; }
    public int NewQuantity { get; }
    public decimal NewPrice { get; }

    public OrderItemUpdated(Guid orderId, Guid productId, int newQuantity, decimal newPrice)
    {
        OrderId = orderId;
        ProductId = productId;
        NewQuantity = newQuantity;
        NewPrice = newPrice;
    }
}