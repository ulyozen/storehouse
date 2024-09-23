using Warehouse360.Core.OrderManagement.Entities;

namespace Warehouse360.Core.OrderManagement.Services;

public interface IOrderService
{
    Task<Order> CreateOrder(Guid customerId);
    Task AddProductToOrder(Guid orderId, Guid productId, int quantity);
    Task CompleteOrder(Guid orderId);
    Task CancelOrder(Guid orderId);
}