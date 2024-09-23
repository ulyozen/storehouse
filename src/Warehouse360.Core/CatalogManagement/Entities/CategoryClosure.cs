namespace Warehouse360.Core.CatalogManagement.Entities;

public class CategoryClosure
{
    public Guid AncestorId { get; private set; }
    public Guid DescendantId { get; private set; }
    public int Depth { get; private set; }

    public CategoryClosure(Guid ancestorId, Guid descendantId, int depth)
    {
        AncestorId = ancestorId;
        DescendantId = descendantId;
        Depth = depth;
    }
    
    public static List<CategoryClosure> CreateClosureForNewCategory(Guid categoryId, Guid? parentId = null)
    {
        var closures = new List<CategoryClosure>();

        closures.Add(new CategoryClosure(categoryId, categoryId, 0));

        if (parentId.HasValue)
        {
            closures.Add(new CategoryClosure(parentId.Value, categoryId, 1));
        }

        return closures;
    }
    
    public static List<CategoryClosure> MoveCategory(Guid categoryId, Guid newParentId)
    {
        var closures = new List<CategoryClosure>
        {
            new CategoryClosure(categoryId, categoryId, 0),
            new CategoryClosure(newParentId, categoryId, 1)
        };

        return closures;
    }

    public static List<CategoryClosure> DeleteCategory(Guid categoryId)
    {
        // Логика для удаления всех записей в таблице CategoryClosure, где categoryId является либо предком, либо потомком

        // Пример: 
        // DELETE FROM CategoryClosure 
        // WHERE AncestorId = categoryId OR DescendantId = categoryId;

        // Возвращаем список удалённых связей, если это нужно для дальнейшей обработки
        return new List<CategoryClosure>();  // Реальная логика будет зависеть от базы данных
    }
}