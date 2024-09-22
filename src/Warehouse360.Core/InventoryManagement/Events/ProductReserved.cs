namespace Warehouse360.Core.InventoryManagement.Events;

public class ProductReserved
{
    public Guid ProductId { get; }
    public int Quantity { get; }

    public ProductReserved(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}