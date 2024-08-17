using System.Linq.Expressions;

namespace PermissionManager.Core.Data.Repositories.Interfaces
{
    public interface IRepositoryWrite<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetWithIncludeAsync(
           Expression<Func<T, bool>> predicate,
           params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}