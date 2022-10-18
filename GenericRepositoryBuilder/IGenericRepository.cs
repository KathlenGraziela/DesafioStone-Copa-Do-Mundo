using System.Linq.Expressions;

namespace GenericRepositoryBuilder
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<List<T>> SelectWhereAsync(Expression<Func<T, bool>> filter);
        public Task<List<T>> SelectAllAsync();
        public Task<List<T>> SelectNAsync(int n);
        public Task<T?> FindFirstAsync(Expression<Func<T, bool>> filter);
        public ValueTask<T?> FindAsync(params object?[]? values);
        public void Add(T entity);
        public void Update(T entity);
        public void Remove(T entity);
        public Task AddAndSaveAsync(T entity);
        public Task UpdateAndSaveAsync(T entity);
        public Task RemoveAndSaveAsync(T entity);
        public Task SaveChangesAsync();
    }
}
