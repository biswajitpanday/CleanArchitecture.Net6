using DotNetCore.Repositories;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> : IRepository<T> where T : class
    {
        Task<T> AddAsync (T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> UpdateAsync (T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> DeleteAsync (T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> DeleteRangeAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
