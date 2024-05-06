using Wajbah_API.Models.DTO;

namespace Wajbah_API.Repository.IRepository
{
    public interface IItemRateRepository
    {
         Task<bool> SetRate(ItemRateRecordDto itemRateRecordDto);
         Task<List<ItemRateRecordDto>> GetAllRates();
    }
}
