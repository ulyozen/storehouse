using System.Data;

namespace Warehouse360.Core.SeedWork.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; }
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}