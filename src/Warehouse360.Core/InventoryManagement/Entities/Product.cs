using Warehouse360.Core.InventoryManagement.Enums;
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

    public Product(string name, string description, Money price, Dimensions dimensions)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Dimensions = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
        Status = ProductStatus.Available;
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