using Warehouse360.Core.OrderManagement.Entities;

namespace Warehouse360.Core.OrderManagement.Services;

public interface IOrderService
{
    Order CreateOrder(Guid customerId);
    void AddProductToOrder(Guid orderId, Guid productId, int quantity);
    void CompleteOrder(Guid orderId);
    void CancelOrder(Guid orderId);
}