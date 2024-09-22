namespace Warehouse360.Core.InventoryManagement.Events;

public class ProductAddedToInventory
{
    public Guid ProductId { get; }
    public string ProductName { get; }
    public int Quantity { get; }

    public ProductAddedToInventory(Guid productId, string productName, int quantity)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
    }
}