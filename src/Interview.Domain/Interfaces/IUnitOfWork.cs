namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        IAsyncRepository<T> AsyncRepository<T>() where T : class;
    }
}