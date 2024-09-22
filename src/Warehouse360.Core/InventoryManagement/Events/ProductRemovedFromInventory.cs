namespace Warehouse360.Core.InventoryManagement.Events;

public class ProductRemovedFromInventory
{
    public Guid ProductId { get; }

    public ProductRemovedFromInventory(Guid productId)
    {
        ProductId = productId;
    }
}