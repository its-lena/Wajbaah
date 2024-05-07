using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using System.Net;
using Wajbah_API.Models;
using Wajbah_API.Models.DTO;
using Wajbah_API.Repository.IRepository;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using Wajbah_API.Data;

namespace Wajbah_API.Controllers
{
	[Route("api/Chef")]
	[ApiController]
	public class ChefController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IChefRepository _dbChef;
		private readonly IMenuItemRepository _dbMenuItems;
		private readonly IPromoCodeRepository _dbPromo;
		private readonly ApplicationDbContext _db;
		private readonly APIResponse _response;
		public ChefController(IChefRepository dbChef, IMapper mapper, IMenuItemRepository dbMenuItems, IPromoCodeRepository dbPromo, ApplicationDbContext db)
		{
			_mapper = mapper;
			_dbChef = dbChef;
			_dbMenuItems = dbMenuItems;
			_dbPromo = dbPromo;
			_db = db;
			this._response = new APIResponse();
		}

		//[HttpPost(Name = "CreateChef")]
		//[ProducesResponseType(StatusCodes.Status201Created)]
		//[ProducesResponseType(StatusCodes.Status400BadRequest)]
		//public async Task<ActionResult<APIResponse>> CreateChef([FromBody] ChefCreateDto chefCreateDto)
		//{
		//	try
		//	{
		//		if (!ModelState.IsValid)
		//		{
		//			return BadRequest(ModelState);
		//		}
		//		/*if (await _dbChef.GetAsync(c => c.NationalId == chefCreateDto.NationalId) != null)
		//              {
		//                  ModelState.AddModelError("ExstingError", "this national ID registered before");
		//                  return BadRequest(ModelState);
		//              }*/
		//		if (await _dbChef.GetAsync(c => c.Email == chefCreateDto.Email.ToLower()) != null)
		//		{
		//			ModelState.AddModelError("ExstingError", "this Email registered before");
		//			return BadRequest(ModelState);
		//		}
		//		else if (await _dbChef.GetAsync(c => c.PhoneNumber == chefCreateDto.PhoneNumber) != null)
		//		{
		//			ModelState.AddModelError("ExstingError", "this phone number registered before");
		//			return BadRequest(ModelState);
		//		}
		//		else
		//		{
		//			Chef chef = _mapper.Map<Chef>(chefCreateDto);
		//			chef.ProfilePicture = "";
		//			chef.Email = chefCreateDto.Email.ToLower();
		//			await _dbChef.CreateAsync(chef);
		//			return Ok();
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		_response.IsSuccess = false;
		//		_response.ErrorMessages = new List<string>() { ex.ToString() };
		//		_response.StatusCode = HttpStatusCode.BadRequest;
		//		return _response;
		//	}
		//}



		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetChefs()
		{
			try
			{

				List<Chef> chefs = await _dbChef.GetAllAsync();
				if (chefs == null)
				{
					ModelState.AddModelError("ExstingError", "There are no Chefs yet");
					return NotFound(ModelState);
				}

				_response.StatusCode = HttpStatusCode.OK;
				List<ChefDto> chefGetAsync = _mapper.Map<List<ChefDto>>(chefs);
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


		[HttpGet("{id}", Name = "GetChef")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetChef(string id)
		{
			try
			{
				//////////هنا
				Chef model = await _dbChef.GetAsync(c => c.ChefId == id);
				model.MenuItems = await _dbMenuItems.GetAllAsync(m => m.ChefId == model.ChefId);
				//var promoCodes = _db.PromoCodes.Select(p => p.ChefPromoCodes.Where(c => c.ChefId == model.ChefId));
				//model.ChefPromoCodes= promoCodes.ToArray();
				
				if (model == null)
				{
					ModelState.AddModelError("ExstingError", "There is no account with this national ID");
					return NotFound(ModelState);
				}
				
				_response.StatusCode = HttpStatusCode.OK;
				ChefDto chefGetAsync = _mapper.Map<ChefDto>(model);
				_response.Result = chefGetAsync;
				//return _response;

				// Serialize the _response object to JSON with Preserve reference handling
				var jsonSettings = new JsonSerializerSettings
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				};
				var jsonResponse = JsonConvert.SerializeObject(_response, jsonSettings);

				// Return the serialized JSON response
				return Content(jsonResponse, "application/json");

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
        [HttpPost("UpdateChefRate", Name = "UpdateChefRate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateChefRate(string id , double newRating)
        {
            try
            {
                if (newRating <= 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var chef = await _dbChef.GetAsync(c=>c.ChefId==id);
                if (chef == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }


                chef.Rating = _dbMenuItems.UpdateRate(newRating, chef.Rating); // It has the same logic for updating the Chef Rating 
                await _dbChef.UpdateAsync(chef);
                _response.Result =chef.Rating;
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
		
		[HttpPost("ActiveSwitch", Name = "ActiveSwitch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> ActiveSwitch(string id )
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
				bool result =await _dbChef.ToggleActiveAsync(id);
                if (result == false)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result =true;
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
    }
}