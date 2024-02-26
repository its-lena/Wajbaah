using Wajbah_API.Data;
using Wajbah_API.Models;

namespace Wajbah_API.Repository.IRepository
{
    public class ChefRepository : Repository<Chef>, IChefRepository
    {
        private readonly ApplicationDbContext _db;
        public ChefRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Chef> UpdateAsync(Chef entity)
        {
            _db.Chefs.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
