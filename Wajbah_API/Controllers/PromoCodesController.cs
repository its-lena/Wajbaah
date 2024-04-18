using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;
        protected APIResponse _response;
        private readonly ApplicationDbContext _db;


        public PromoCodesController(IPromoCodeRepository dbItem, IMapper mapper, ApplicationDbContext db)
        {
            _dbItem = dbItem;
            _mapper = mapper;
            this._response = new();
            _db = db;
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

                if (promoCodeCreate == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                PromoCode promoCode = _mapper.Map<PromoCode>(promoCodeCreate);
                await _dbItem.CreateAsync(promoCode);

                _response.Result = _mapper.Map<PromoCodeDTO>(promoCodeCreate);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("", new { id = promoCode.PromoCodeId }, _response);
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
