using Warehouse360.Core.CatalogManagement.Entities;
using Warehouse360.Core.CatalogManagement.Repositories;

namespace Warehouse360.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public Task AddAsync(Category category, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Category category, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}