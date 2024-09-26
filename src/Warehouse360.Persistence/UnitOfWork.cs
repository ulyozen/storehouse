using System.Data;
using Warehouse360.Core.SeedWork.Interfaces;
using Warehouse360.Persistence.Configurations;

namespace Warehouse360.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private IDbConnection? _connection;
    private IDbTransaction? _transaction;
    private bool _disposed;

    public UnitOfWork(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public IDbConnection Connection => _connection ?? throw new InvalidOperationException("Connection is not available.");
    public IDbTransaction Transaction => _transaction ?? throw new InvalidOperationException("Transaction is not available.");
    
    public async Task BeginTransactionAsync()
    {
        _connection = await _dbConnectionFactory.CreateConnectionAsync();
        _transaction = _connection.BeginTransaction();
    }

    public Task CommitTransactionAsync()
    {
        if (_transaction == null) throw new InvalidOperationException("Transaction has not been started.");
        _transaction.Commit();
        Dispose();
        return Task.CompletedTask;
    }

    public Task RollbackTransactionAsync()
    {
        if (_transaction == null) throw new InvalidOperationException("Transaction has not been started.");
        _transaction.Rollback();
        Dispose();
        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _connection?.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}