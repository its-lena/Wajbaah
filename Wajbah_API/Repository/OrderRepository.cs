using Wajbah_API.Data;
using Wajbah_API.Models;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Repository
{
	public class OrderRepository : Repository<Order>, IOrderRepository
	{
		private readonly ApplicationDbContext _db;
		public OrderRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task<Order> UpdateAsync(Order entity)
		{
			_db.Orders.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
