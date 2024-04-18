using Wajbah_API.Models;

namespace Wajbah_API.Repository.IRepository
{
    public interface ICustomerRepository: IRepository<Customer>
    {
        Task<Customer> UpdateAsync(Customer entity);
    }
}
