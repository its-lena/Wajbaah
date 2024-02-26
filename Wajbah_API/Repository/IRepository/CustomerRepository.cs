using Wajbah_API.Data;
using Wajbah_API.Models;

namespace Wajbah_API.Repository.IRepository
{
    public class CustomerRepository:Repository<Customer>, ICustomerRepository
    {

        private readonly ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Customer> UpdateAsync(Customer entity)
        {
            _db.Customers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
