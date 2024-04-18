using Wajbah_API.Models;

namespace Wajbah_API.Repository.IRepository
{
    public interface IChefRepository : IRepository<Chef>
    {
        Task<Chef> UpdateAsync(Chef entity);
    }
}
