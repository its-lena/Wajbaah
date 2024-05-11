using AutoMapper;
using Wajbah_API.Data;
using Wajbah_API.Models;
using Wajbah_API.Models.DTO;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Repository
{
    public class ItemRateRepository : IItemRateRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public ItemRateRepository(ApplicationDbContext db, IMapper mapper) 
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<List<ItemRateRecordDto>> GetAllRates()
        {
            var records = _mapper.Map<List<ItemRateRecordDto>>(await _db.ItemRateRecords.ToListAsync());
            return records;
        }

        public async Task<bool> SetRate(ItemRateRecordDto itemRateRecordDto)
        {
            var model= await _db.ItemRateRecords.FirstOrDefaultAsync(i=>i.CustomerId== itemRateRecordDto.CustomerId && i.MenuItemId== itemRateRecordDto.MenuItemId);
            if(model!=null)
            {
                model.Rating = itemRateRecordDto.Rating;
                _db.ItemRateRecords.Update(model);
            }
            else
            {
                ItemRateRecord itemRateRecord = _mapper.Map<ItemRateRecord>(itemRateRecordDto);
                await _db.ItemRateRecords.AddAsync(itemRateRecord);
            }
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
