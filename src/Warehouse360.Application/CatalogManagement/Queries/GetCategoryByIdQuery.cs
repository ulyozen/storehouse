using MediatR;
using Warehouse360.Application.CatalogManagement.Dtos;

namespace Warehouse360.Application.CatalogManagement.Queries;

public class GetCategoryByIdQuery : IRequest<CategoryDto>
{
    public Guid CategoryId { get; }

    public GetCategoryByIdQuery(Guid categoryId)
    {
        CategoryId = categoryId;
    }
}