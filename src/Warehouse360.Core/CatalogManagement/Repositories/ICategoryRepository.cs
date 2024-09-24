using Warehouse360.Core.CatalogManagement.Entities;

namespace Warehouse360.Core.CatalogManagement.Repositories;

public interface ICategoryRepository
{
    Task AddAsync(Category category, CancellationToken cancellationToken);

    Task UpdateAsync(Category category, CancellationToken cancellationToken);

    Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);

    Task DeleteAsync(Category category, CancellationToken cancellationToken);
}