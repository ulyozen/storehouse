namespace Warehouse360.Core.InventoryManagement.Events;

public class ProductPriceUpdated
{
    public Guid ProductId { get; }
    public decimal NewPrice { get; }

    public ProductPriceUpdated(Guid productId, decimal newPrice)
    {
        ProductId = productId;
        NewPrice = newPrice;
    }
}