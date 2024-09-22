namespace Warehouse360.Core.SeedWork.Interfaces;

public interface IRepository<T> where T : IAggregateRoot
{
    Task<T> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}