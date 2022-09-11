namespace Domain.Interfaces;

public interface IAsyncRepository<T> where T : class
{
    DbSet<T> Entity { get; }
}