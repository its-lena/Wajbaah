using Wajbah_API.Models;

namespace Wajbah_API.Repository.IRepository
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task<Order> UpdateAsync(Order entity);
	}
}
