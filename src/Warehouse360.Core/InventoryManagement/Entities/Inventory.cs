using Warehouse360.Core.InventoryManagement.Enums;
using Warehouse360.Core.InventoryManagement.Events;

namespace Warehouse360.Core.InventoryManagement.Entities;

public class Inventory
{
    public string Location { get; private set; }
    public InventoryStatus Status { get; private set; }
    public List<Product> Products { get; private set; }

    public Inventory(string location)
    {
        Location = location ?? throw new ArgumentNullException(nameof(location));
        Products = new List<Product>();
        Status = InventoryStatus.Empty;
    }

    public void AddProduct(Product product)
    {
        if (Products.Any(p => p.Id == product.Id))
            throw new InvalidOperationException("Product already exists in the inventory");

        Products.Add(product);
        UpdateStatus();
        
        var productEvent = new ProductAddedToInventory(product.Id, product.Name, product.Quantity);
        // Логика для публикации события
    }

    public void RemoveProduct(Product product)
    {
        if (!Products.Contains(product))
            throw new InvalidOperationException("Product not found in the inventory");

        if (product.Status == ProductStatus.OutOfStock)
            throw new InvalidOperationException("Cannot remove product that is out of stock");

        Products.Remove(product);
        UpdateStatus();
    }

    public void ReplenishStock(Product product, int quantity)
    {
        if (!Products.Contains(product))
            throw new InvalidOperationException("Product not found in the inventory");

        product.UpdateQuantityIncrease(quantity);
        product.MarkAsAvailable();
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        Status = Products.Count > 0 ? InventoryStatus.Full : InventoryStatus.Empty;
    }
}