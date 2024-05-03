using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net;
using Wajbah_API.Data;
using Wajbah_API.Models;
using Wajbah_API.Models.DTO;
using Wajbah_API.Models.DTO.PromoCode;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodesController : ControllerBase
    {
        private readonly IPromoCodeRepository _dbItem;
        private readonly IChefRepository _dbChef;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        private readonly ApplicationDbContext _db;


        public PromoCodesController(IPromoCodeRepository dbItem, IMapper mapper, ApplicationDbContext db, IChefRepository dbChef)
        {
            _dbItem = dbItem;
            _mapper = mapper;
            this._response = new();
            _db = db;
            _dbChef = dbChef;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetPromoCodes()
        {
            try
            {
                IEnumerable<PromoCode> promoCodes = await _dbItem.GetAllAsync();
                _response.Result = _mapper.Map<List<PromoCodeDTO>>(promoCodes);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.Message };
            }
            return _response;
        }

		[HttpGet("{id}", Name = "GetPromocode")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetPromoCode(int id)
		{
			try
			{
				PromoCode model = await _dbItem.GetAsync(i => i.PromoCodeId == id);

				if (model == null)
				{
					ModelState.AddModelError("ExstingError", "There is no promocode with this ID");
					return NotFound(ModelState);
				}

				_response.StatusCode = HttpStatusCode.OK;
				PromoCodeDTO promocode = _mapper.Map<PromoCodeDTO>(model);

				//if (model.Chefs != null)
				//{
				//	promocode.ChefIds = model.Chefs.Select(c => c.ChefId).ToList();
				//}
				//else
				//{
				//	promocode.ChefIds = new List<string>(); // or null, depending on your preference
				//}
				_response.Result = promocode;
				return _response;

			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
				_response.StatusCode = HttpStatusCode.BadRequest;
				return _response;
			}
		}

		[HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreatePromoCode([FromBody] PromoCodeCreateDTO promoCodeCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _dbItem.GetAsync(u => u.Name.ToLower() == promoCodeCreate.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("Custom-Error", "PromoCode Name already exsists");
                    return BadRequest(ModelState);
                }

                //if (promoCodeCreate == null)
                //{
                //    _response.IsSuccess = false;
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    return BadRequest(_response);
                //}

                PromoCode promoCode = new PromoCode
                {
                    Name = promoCodeCreate.Name,
                    StartDate= promoCodeCreate.StartDate,
                    ExpireDate= promoCodeCreate.ExpireDate,
                    DiscountPercentage= promoCodeCreate.DiscountPercentage,
                    MaxUsers= promoCodeCreate.MaxUsers,
                    MinLimit= promoCodeCreate.MinLimit,
                    MaxLimit= promoCodeCreate.MaxLimit
                };

				ICollection<Chef> chefs = await _dbChef.GetAllAsync(c => promoCodeCreate.ChefIds.Contains(c.ChefId));

                if(chefs.Count != promoCodeCreate.ChefIds.Count)
                {
					ModelState.AddModelError("Custom-Error", "Invalid chef(s) ID");
					return BadRequest(ModelState);
				}

                promoCode.Chefs = chefs.ToList();
                await _dbItem.CreateAsync(promoCode);

				_response.Result = _mapper.Map<PromoCodeDTO>(promoCode);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetPromocode", new { id = promoCode.PromoCodeId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> UpdatePromoCode(int id, [FromBody] PromoCodeUpdateDTO promoCodeUpdate)
        {
            try
            {
                if (id != promoCodeUpdate.PromoCodeId || promoCodeUpdate == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                PromoCode promoCode = _mapper.Map<PromoCode>(promoCodeUpdate);
                await _dbItem.UpdateAsync(promoCode);

                _response.Result = _mapper.Map<PromoCodeUpdateDTO>(promoCode);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.Message };
            }
            return _response;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> DeletePromoCode(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                PromoCode promoCode = await _dbItem.GetAsync(u => u.PromoCodeId == id);
                if (promoCode == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _dbItem.RemoveAsync(promoCode);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.Message };
            }
            return _response;
        }
    }
}
