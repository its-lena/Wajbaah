using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wajbah_API.Models;
using Wajbah_API.Models.DTO;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Controllers
{
    [Route("api/Chef")]
    [ApiController]
    public class ChefController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IChefRepository _dbChef;
        private readonly APIResponse _response;
        public ChefController(IChefRepository dbChef, IMapper mapper)
        {
            _mapper = mapper;
            _dbChef = dbChef;
            this._response = new APIResponse();
        }
        [HttpGet(Name = "GetAllChefs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAllChefs()
        {
            try
            {

                IEnumerable<Chef> model = await _dbChef.GetAllAsync();
                _response.Result = _mapper.Map<List<ChefDto>>(model);
                if (model == null)
                {
                    return NotFound();
                }

                _response.StatusCode = HttpStatusCode.OK;
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


        [HttpPost(Name = "CreateChef")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateChef([FromBody] ChefCreateDto chefCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                /*if (await _dbChef.GetAsync(c => c.NationalId == chefCreateDto.NationalId) != null)
                {
                    ModelState.AddModelError("ExstingError", "this national ID registered before");
                    return BadRequest(ModelState);
                }*/
                if (await _dbChef.GetAsync(c => c.Email == chefCreateDto.Email.ToLower()) != null)
                {
                    ModelState.AddModelError("ExstingError", "this Email registered before");
                    return BadRequest(ModelState);
                }
                else if (await _dbChef.GetAsync(c => c.PhoneNumber == chefCreateDto.PhoneNumber) != null)
                {
                    ModelState.AddModelError("ExstingError", "this phone number registered before");
                    return BadRequest(ModelState);
                }
                else
                {
                    Chef chef = _mapper.Map<Chef>(chefCreateDto);
                    chef.ProfilePicture = "";
                    chef.Email = chefCreateDto.Email.ToLower();
                    await _dbChef.CreateAsync(chef);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return _response;
            }
        }
        [HttpGet("{id}", Name = "GetChef")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetChef(string id)
        {
            try
            {

                Chef model = await _dbChef.GetAsync(c => c.ChefId == id);
                if (model == null)
                {
                    ModelState.AddModelError("ExstingError", "There is no account with this national ID");
                    return NotFound(ModelState);
                }

                _response.StatusCode = HttpStatusCode.OK;
                ChefDto chefGetAsync = _mapper.Map<ChefDto>(model);
                _response.Result = chefGetAsync;
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

        [HttpPut("{id}", Name = "UpdateChef")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateChef(string id, [FromBody] ChefUpdateDto chefUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Chef model = await _dbChef.GetAsync(c => c.ChefId == id, false);
                if (model == null)
                {
                    ModelState.AddModelError("ExstingError", "There is no account with this natinal id");
                    return NotFound(ModelState);
                }
                model = _mapper.Map<Chef>(chefUpdate);
                model.ChefId = id;
                model.ProfilePicture = "";
                await _dbChef.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = chefUpdate;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return _response;
            }
        }

        [HttpDelete("{id}", Name = "DeleteChef")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteChef(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Chef model = await _dbChef.GetAsync(c => c.ChefId == id, false);
                if (model == null)
                {
                    ModelState.AddModelError("ExstingError", "There is no account with this natinal id");
                    return NotFound(ModelState);
                }
                await _dbChef.RemoveAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.Result = "Deleted Successfuly";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return _response;
            }
        }
    }
}
