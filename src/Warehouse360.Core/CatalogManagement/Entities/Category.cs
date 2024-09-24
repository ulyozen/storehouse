using Warehouse360.Core.CatalogManagement.Enums;
using Warehouse360.Core.SeedWork.Entities;

namespace Warehouse360.Core.CatalogManagement.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ImageUrl { get; private set; }
    public CategoryType CategoryType { get; private set; }
    public Category Parent { get; private set; }
    public List<ProductCategory> ProductCategories { get; private set; }

    public Category(string name, string description)
    {
        Name = name;
        Description = description;
        ProductCategories = new List<ProductCategory>();
    }
    
    public Category(string name, string description, string imageUrl, CategoryType categoryType, Category parent = null)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        CategoryType = categoryType;
        Parent = parent;
        ProductCategories = new List<ProductCategory>();
    }
    
    public void AddProduct(Product product)
    {
        if (ProductCategories.Any(pc => pc.ProductId == product.Id))
            throw new InvalidOperationException("Product is already in this category.");

        ProductCategories.Add(ProductCategory.Create(product, this));
    }
    
    public void RemoveProduct(Product product)
    {
        var productCategory = ProductCategories.SingleOrDefault(pc => pc.ProductId == product.Id);
        if (productCategory == null)
            throw new InvalidOperationException("Product is not in this category.");

        ProductCategories.Remove(productCategory);
    }
    
    public static Category Create(string name, string description, string imageUrl, CategoryType categoryType, Category parent = null)
    {
        return new Category(name, description, imageUrl, categoryType, parent);
    }
    
    public void UpdateProperties(string description, string imageUrl)
    {
        Description = description;
        ImageUrl = imageUrl;
    }
    
    public void MoveTo(Category newParent, List<CategoryClosure> closures)
    {
        if (newParent == null)
            throw new ArgumentNullException(nameof(newParent));

        Parent = newParent;

        closures = CategoryClosure.MoveCategory(Id, newParent.Id);
    }

    public void DeleteCategory(List<CategoryClosure> closures)
    {
        // Удаляем все записи из CategoryClosure, где категория является предком или потомком
        closures = CategoryClosure.DeleteCategory(this.Id);

        // Дополнительная логика для удаления самой категории и её подкатегорий
        // (например, удаление из базы данных, каскадное удаление)
    }
}