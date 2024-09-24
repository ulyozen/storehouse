using MediatR;
using Warehouse360.Application.CatalogManagement.Dtos;

namespace Warehouse360.Application.CatalogManagement.Queries;

public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;