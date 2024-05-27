using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Web.Mvc;
using Wajbah_API.Data;

namespace WajbahAdmin.Service
{
    public class UserHandelService<T> : IUserHandelService<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public UserHandelService(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }


        public async Task<List<T>> Search(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Query(filter, includeProperties);

            return await query.ToListAsync();
        }
        public async Task<T> Get(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query=Query(filter, includeProperties);

            return await query.FirstOrDefaultAsync(); 
        }

        public IQueryable<T> Query(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        /* public async Task<Customer> UpdateAsync(Customer entity)
        {
            _db.Customers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }*/
    }
}
