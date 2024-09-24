namespace Warehouse360.Core.SeedWork.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangeAsync(CancellationToken cancellationToken);

    Task RollbackAsync(CancellationToken cancellationToken);
}