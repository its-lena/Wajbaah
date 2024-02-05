using Wajbah_API.Models;

namespace Wajbah_API.Repository.IRepository
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        Task<MenuItem> UpdateAsync(MenuItem entity);
    }
}
