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

        public async Task<bool> ToggleActiveAsync(string id)
        {
            Chef chef = await GetAsync(c => c.ChefId == id);
            if(chef != null)
            {
                if(chef.Active==true)
                {
                    chef.Active = false;
                }
                else
                {
                    chef.Active = true;
                }
                await UpdateAsync(chef);
                return true;
            }
            return false;
        }

        public async Task<Chef> UpdateAsync(Chef entity)
        {
            _db.Chefs.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
