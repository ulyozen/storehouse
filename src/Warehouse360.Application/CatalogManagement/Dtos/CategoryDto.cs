namespace Warehouse360.Application.CatalogManagement.Dtos;

public class CategoryDto(Guid categoryId, string name, string description)
{
    public Guid Id { get; set; } = categoryId;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
}