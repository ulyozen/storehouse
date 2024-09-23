using Warehouse360.Core.SeedWork.Entities;

namespace Warehouse360.Core.CatalogManagement.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; }
    public List<ProductCategory> ProductCategories { get; private set; } = new();

    public void AddToCategory(Category category)
    {
        if (ProductCategories.Any(pc => pc.CategoryId == category.Id))
            throw new InvalidOperationException("Product is already in this category.");

        ProductCategories.Add(ProductCategory.Create(this, category));
    }

    public void RemoveFromCategory(Category category)
    {
        var productCategory = ProductCategories.SingleOrDefault(pc => pc.CategoryId == category.Id);
        if (productCategory == null)
            throw new InvalidOperationException("Product is not in this category.");

        ProductCategories.Remove(productCategory);
    }
}