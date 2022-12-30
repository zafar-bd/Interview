using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;
    protected RestaurantEFContext DbContext { get; }
    public RepositoryBase(RestaurantEFContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
        DbContext = dbContext;
    }

    public DbSet<T> Entity => _dbSet;
}