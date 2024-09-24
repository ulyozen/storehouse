using MediatR;

namespace Warehouse360.Application.CatalogManagement.Commands;

public class UpdateCategoryCommand : IRequest
{
    public Guid CategoryId { get; }
    public string Name { get; }
    public string Description { get; }

    public UpdateCategoryCommand(Guid categoryId, string name, string description)
    {
        CategoryId = categoryId;
        Name = name;
        Description = description;
    }
}