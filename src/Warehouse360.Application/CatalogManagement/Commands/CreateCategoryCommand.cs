using MediatR;
using Warehouse360.Core.CatalogManagement.Enums;

namespace Warehouse360.Application.CatalogManagement.Commands;

public class CreateCategoryCommand : IRequest<Guid>
{
    public string Name { get; }
    public string Description { get; }

    public CreateCategoryCommand(string name, string description)
    {
        Name = name;
        Description = description;
    }
}