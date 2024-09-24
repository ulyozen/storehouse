using Warehouse360.Application.CatalogManagement.Dtos;

namespace Warehouse360.Application.CatalogManagement.Abstractions;

public interface ICategoryCacheService
{
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken);
    Task SetCategoriesAsync(IEnumerable<CategoryDto> categories, CancellationToken cancellationToken);
    Task<CategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);
    Task SetCategoryAsync(CategoryDto category,CancellationToken cancellationToken);
}