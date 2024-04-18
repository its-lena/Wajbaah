using Wajbah_API.Models;

namespace Wajbah_API.Repository.IRepository
{
	public interface IExtraMenuItemRepository : IRepository<ExtraMenuItem>
	{
		Task<ExtraMenuItem> UpdateAsync(ExtraMenuItem entity);
	}
}
