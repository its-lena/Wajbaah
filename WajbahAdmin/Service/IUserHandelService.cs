using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WajbahAdmin.Service
{
    public interface IUserHandelService <T> where T : class
    {
        Task<List<T>> Search(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> Get(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> Query(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties);
        Task SaveAsync();
    }
}
