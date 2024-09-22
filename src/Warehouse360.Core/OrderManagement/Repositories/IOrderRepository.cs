using Warehouse360.Core.OrderManagement.Entities;

namespace Warehouse360.Core.OrderManagement.Repositories;

public interface IOrderRepository
{
    Task AddOrderAsync(Order order);
    Task<Order> GetOrderByIdAsync(Guid orderId);
    Task UpdateOrderAsync(Order order);
}