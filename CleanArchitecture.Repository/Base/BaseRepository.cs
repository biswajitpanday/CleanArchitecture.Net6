using System.Linq.Expressions;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Repository.DatabaseContext;
using DotNetCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CleanArchitecture.Repository.Base
{
    public class BaseRepository<T> : EFRepository<T>, IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IQueryable<T?> _queryable;

        public BaseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _queryable = _context.QuerySet<T?>();
        }

        // Commands
        public new void Add(T item)
        {
            item.CreatedAt = DateTime.UtcNow;
            item.LastUpdate = DateTime.UtcNow;
            _dbSet.Add(item);
        }

        public new async Task AddAsync(T item)
        {
            item.CreatedAt = DateTime.UtcNow;
            item.LastUpdate = DateTime.UtcNow;
            await _dbSet.AddAsync(item);
        }

        public new void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                item.CreatedAt = DateTime.UtcNow;
                item.LastUpdate = DateTime.UtcNow;
            }
            _dbSet.AddRange(items);
        }

        public new async Task AddRangeAsync(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                item.CreatedAt = DateTime.UtcNow;
                item.LastUpdate = DateTime.UtcNow;
            }
            await _dbSet.AddRangeAsync(items);
        }

        public void HardDelete(object key)
        {
            var entity = _dbSet.Find(key);
            if (entity == null)
                return;
            _dbSet.Remove(entity);
        }

        public void SoftDelete(object key)
        {
            var entity = _dbSet.Find(key);
            if (entity == null)
                return;
            entity.IsDeleted = true;
            UpdatePartial(entity);
        }

        public void HardDelete(Expression<Func<T, bool>> where)
        {
            var queryable = _dbSet.Where<T>(where);
            if (!queryable.Any<T>())
                return;
            _dbSet.RemoveRange(queryable);
        }

        public void SoftDelete(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
            // var queryable = _dbSet.Where<T>(where); // todo: Need to Implement.
            // if (!queryable.Any<T>())
            //     return;
            //_dbSet.RemoveRange(queryable);    
        }

        public Task HardDeleteAsync(object key)
        {
            return Task.Run((Action)(() => HardDelete(key)));
        }

        public Task SoftDeleteAsync(object key)
        {
            return Task.Run((Action)(() => SoftDelete(key)));
        }

        public Task HardDeleteAsync(Expression<Func<T, bool>> where)
        {
            return Task.Run((Action)(() => HardDelete(where)));
        }

        public Task SoftDeleteAsync(Expression<Func<T, bool>> where)
        {
            return Task.Run((Action)(() => SoftDelete(where)));
        }

        public new void Update(T item)
        {
            var entity = _dbSet.Find(_context.PrimaryKeyValues<T>(item));
            if (entity == null)
                return;
            entity.LastUpdate = DateTime.UtcNow;
            _context.Entry<T>(entity).State = EntityState.Detached;
            _context.Update<T>(item);
        }

        public new Task UpdateAsync(T item)
        {
            return Task.Run((Action) (() => Update(item)));
        }

        public new void UpdatePartial(object item)
        {
            var entity = _dbSet.Find(_context.PrimaryKeyValues<T>(item));
            if (entity == null)
                return;
            entity.LastUpdate = DateTime.UtcNow;
            var entityEntry = _context.Entry<T>(entity);
            entityEntry.CurrentValues.SetValues(item);
            foreach (var navigation in entityEntry.Metadata.GetNavigations())
            {
                if (!navigation.IsOnDependent && !((IReadOnlyNavigation) navigation).IsCollection &&
                    navigation.ForeignKey.IsOwnership)
                {
                    var property = item.GetType().GetProperty(navigation.Name);
                    if (property != null)
                    {
                        var obj = property.GetValue(item, null);
                        if (obj != null)
                            entityEntry.Reference(navigation.Name).TargetEntry?.CurrentValues.SetValues(obj);
                    }
                }
            }
        }

        public new Task UpdatePartialAsync(object item)
        {
            return Task.Run((Action) (() => UpdatePartial(item)));
        }

        public new void UpdateRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                item.LastUpdate = DateTime.UtcNow;
            }
            _dbSet.UpdateRange(items);
        }

        public new Task UpdateRangeAsync(IEnumerable<T> items)
        {
            return Task.Run((Action) (() => UpdateRange(items)));
        }


        // Queries
        public new bool Any() => _queryable!.Any<T>();

        public new bool Any(Expression<Func<T, bool>> where) => _queryable!.Any<T>(@where);

        public new async Task<bool> AnyAsync() => await _queryable!.AnyAsync<T>();

        public new async Task<bool> AnyAsync(Expression<Func<T, bool>> where) => await _queryable!.AnyAsync<T>(@where);

        public new long Count() => _queryable!.LongCount<T>();

        public new long Count(Expression<Func<T, bool>> where) => _queryable!.LongCount<T>(@where);

        public new async Task<long> CountAsync() => await _queryable!.LongCountAsync<T>();

        public new async Task<long> CountAsync(Expression<Func<T, bool>> where) => await _queryable!.LongCountAsync<T>(@where);

        public T? Get(Guid key) => _context.DetectChangesLazyLoading(false).Set<T>().Find(key);

        public async Task<T?> GetAsync(Guid key) => await _queryable.Where(x => x!.Id == key).SingleOrDefaultAsync();

        public new IEnumerable<T> List() => _queryable!.ToList<T>();

        public new async Task<IEnumerable<T>> ListAsync() => await _queryable!.ToListAsync<T>().ConfigureAwait(false);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Save() => _context.SaveChanges();
    }
}
