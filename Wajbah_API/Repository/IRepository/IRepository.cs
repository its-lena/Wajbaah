using System.Linq.Expressions;

namespace Wajbah_API.Repository.IRepository
{
    public interface IRepository <T> where T : class
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracking = true);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
