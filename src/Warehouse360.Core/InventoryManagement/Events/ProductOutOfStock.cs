namespace Warehouse360.Core.InventoryManagement.Events;

public class ProductOutOfStock
{
    public Guid ProductId { get; }

    public ProductOutOfStock(Guid productId)
    {
        ProductId = productId;
    }
}