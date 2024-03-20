using Wajbah_API.Data;
using Wajbah_API.Models;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Repository
{
	public class ExtraMenuItemRepository : Repository<ExtraMenuItem>, IExtraMenuItemRepository
	{
		private readonly ApplicationDbContext _db;
		public ExtraMenuItemRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task<ExtraMenuItem> UpdateAsync(ExtraMenuItem entity)
		{
			_db.ExtraMenuItems.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
