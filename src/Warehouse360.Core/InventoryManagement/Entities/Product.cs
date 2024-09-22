using Warehouse360.Core.InventoryManagement.Enums;
using Warehouse360.Core.InventoryManagement.Events;
using Warehouse360.Core.InventoryManagement.ValueObjects;
using Warehouse360.Core.SeedWork.Entities;
using Warehouse360.Core.SeedWork.Interfaces;

namespace Warehouse360.Core.InventoryManagement.Entities;

public class Product : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Money Price { get; private set; }
    public Dimensions Dimensions { get; private set; }
    public ProductStatus Status { get; private set; }
    public int Quantity { get; private set; }

    public Product(string name, string description, Money price, Dimensions dimensions, int quantity)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Dimensions = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
        Quantity = quantity;
        Status = quantity > 0 ? ProductStatus.Available : ProductStatus.OutOfStock;
    }
    
    public void Reserve(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        if (Quantity < quantity)
            throw new InvalidOperationException("Not enough product in stock to reserve");

        Quantity -= quantity;
        if (Quantity == 0)
        {
            Status = ProductStatus.OutOfStock;
        }
        
        var productEvent = new ProductReserved(Id, quantity);
        // Логика для публикации события
    }
    
    public void UpdateDescription(string description)
    {
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }
    
    public void UpdateQuantityIncrease(int quantity)
    {
        Quantity += quantity;
    }

    public void UpdatePrice(Money newPrice)
    {
        Price = newPrice ?? throw new ArgumentNullException(nameof(newPrice));
    }

    public void MarkAsOutOfStock()
    {
        Status = ProductStatus.OutOfStock;
    }

    public void MarkAsAvailable()
    {
        Status = ProductStatus.Available;
    }
}