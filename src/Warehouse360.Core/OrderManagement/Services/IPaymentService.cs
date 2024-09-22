using Warehouse360.Core.OrderManagement.Entities;
using Warehouse360.Core.OrderManagement.ValueObjects;

namespace Warehouse360.Core.OrderManagement.Services;

public interface IPaymentService
{
    Task<bool> ProcessPaymentAsync(Order order, PaymentDetails paymentDetails);
}