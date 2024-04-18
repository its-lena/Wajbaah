using Wajbah_API.Models;

namespace Wajbah_API.Repository.IRepository
{
    public interface IPromoCodeRepository : IRepository<PromoCode>
    {
        Task<PromoCode> UpdateAsync(PromoCode entity);
    }
}
