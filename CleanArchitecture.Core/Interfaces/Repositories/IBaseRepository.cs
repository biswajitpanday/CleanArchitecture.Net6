using CleanArchitecture.Core.Entities;
using DotNetCore.Repositories;
using System.Linq.Expressions;

namespace CleanArchitecture.Core.Interfaces.Repositories;

public interface IBaseRepository<T> : IRepository<T> where T : BaseEntity
{
    new void Add(T item);
    new Task AddAsync(T item);
    new void AddRange(IEnumerable<T> items);
    new Task AddRangeAsync(IEnumerable<T> items);
    void HardDelete(object key);
    void SoftDelete(object key);
    void HardDelete(Expression<Func<T, bool>> where);
    void SoftDelete(Expression<Func<T, bool>> where);
    Task HardDeleteAsync(object key);
    Task SoftDeleteAsync(object key);
    Task HardDeleteAsync(Expression<Func<T, bool>> where);
    Task SoftDeleteAsync(Expression<Func<T, bool>> where);
    new void Update(T item);
    new Task UpdateAsync(T item);
    new void UpdatePartial(object item);
    new Task UpdatePartialAsync(object item);
    new void UpdateRange(IEnumerable<T> items);
    new Task UpdateRangeAsync(IEnumerable<T> items);
    new bool Any();
    new bool Any(Expression<Func<T, bool>> where);
    new Task<bool> AnyAsync();
    new Task<bool> AnyAsync(Expression<Func<T, bool>> where);
    new long Count();
    new long Count(Expression<Func<T, bool>> where);
    new Task<long> CountAsync();
    new Task<long> CountAsync(Expression<Func<T, bool>> where);
    T? Get(Guid key);
    Task<T?> GetAsync(Guid key);
    new IEnumerable<T> List();
    new Task<IEnumerable<T>> ListAsync();
    Task SaveChangesAsync();
    void Save();
}