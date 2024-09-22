using Warehouse360.Core.InventoryManagement.Entities;

namespace Warehouse360.Core.InventoryManagement.Repositories;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(Guid productId);
    Task UpdateProductAsync(Product product);
}