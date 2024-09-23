namespace Warehouse360.Core.CatalogManagement.Entities;

public class ProductCategory
{
    public Guid ProductId { get; private set; }
    public Guid CategoryId { get; private set; }

    // Связи с сущностями Product и Category
    public Product Product { get; private set; }
    public Category Category { get; private set; }

    public ProductCategory(Guid productId, Guid categoryId)
    {
        ProductId = productId;
        CategoryId = categoryId;
    }

    // Фабричный метод для создания связи между продуктом и категорией
    public static ProductCategory Create(Product product, Category category)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        return new ProductCategory(product.Id, category.Id);
    }
}