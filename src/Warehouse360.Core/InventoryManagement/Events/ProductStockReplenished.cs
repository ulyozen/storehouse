namespace Warehouse360.Core.InventoryManagement.Events;

public class ProductStockReplenished
{
    public Guid ProductId { get; }
    public int ReplenishedQuantity { get; }

    public ProductStockReplenished(Guid productId, int replenishedQuantity)
    {
        ProductId = productId;
        ReplenishedQuantity = replenishedQuantity;
    }
}