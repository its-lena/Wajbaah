using Microsoft.AspNetCore.Mvc;
using SharpCompress.Common;
using Wajbah_API.Data;
using Wajbah_API.Models;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _db;
        public MenuItemRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public async Task<MenuItem> UpdateAsync(MenuItem entity)
        {
            
            _db.MenuItems.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
        public double UpdateRate( double newRate, double oldRate)
        {
            double smoothingFactor = 0.4;
            double Rate = (newRate - oldRate) * smoothingFactor + oldRate;
            return Rate;
        }
    }
}
