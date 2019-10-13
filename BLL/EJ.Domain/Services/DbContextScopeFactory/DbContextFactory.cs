using System;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EJ.Domain.Services.DbContextScopeFactory
{
    public interface IDbContextFactory : IDisposable
    {
        TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext;
        TDbContext CreateReadonlyDbContext<TDbContext>() where TDbContext : DbContext;
    }

    public class DbContextFactory : IDbContextFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentBag<IDisposable> _disposables = new ConcurrentBag<IDisposable>();

        private bool _isDisposed;

        public DbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext
        {
            return GetScopedContext<TDbContext>();
        }

        public TDbContext CreateReadonlyDbContext<TDbContext>() where TDbContext : DbContext
        {
            var context = GetScopedContext<TDbContext>();
            context.ChangeTracker.AutoDetectChangesEnabled = false;

            return context;
        }

        private TDbContext GetScopedContext<TDbContext>() where TDbContext : DbContext
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(DbContextFactory));

            var scope = _serviceProvider.CreateScope();
            _disposables.Add(scope);

            return scope.ServiceProvider.GetRequiredService<TDbContext>();
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            _isDisposed = true;

            lock (_disposables)
            {
                foreach (var disposable in _disposables)
                {
                    disposable.Dispose();
                }

                _disposables.Clear();
            }
        }
    }
}
