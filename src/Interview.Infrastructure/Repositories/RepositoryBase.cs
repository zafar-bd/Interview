namespace Infrastructure.Data.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(EFContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        public DbSet<T> Entity => _dbSet;
    }
}