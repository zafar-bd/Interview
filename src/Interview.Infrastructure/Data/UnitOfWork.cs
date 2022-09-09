namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantEFContext _dbContext;

        public UnitOfWork(RestaurantEFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAsyncRepository<T> AsyncRepository<T>() where T : class
        {
            return new RepositoryBase<T>(_dbContext);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}