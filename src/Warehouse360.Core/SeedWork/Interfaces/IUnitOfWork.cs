namespace Warehouse360.Core.SeedWork.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}