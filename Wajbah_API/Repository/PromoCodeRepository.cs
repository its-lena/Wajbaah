using Wajbah_API.Data;
using Wajbah_API.Models;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Repository
{
    public class PromoCodeRepository : Repository<PromoCode>, IPromoCodeRepository
    {
        private readonly ApplicationDbContext _db;
        public PromoCodeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<PromoCode> UpdateAsync(PromoCode entity)
        {
            _db.PromoCodes.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
