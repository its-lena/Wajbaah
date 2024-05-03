using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using Wajbah_API.Data;
using Wajbah_API.Models;
using Wajbah_API.Models.DTO;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Controllers
{
	[ApiController]
	[Route("api/ExtraMenuItem")]
	public class ExtraMenuItemController : ControllerBase
	{
		private readonly IExtraMenuItemRepository _dbExtraItem;
		private readonly IMapper _mapper;
		protected APIResponse _response;
		private readonly ApplicationDbContext _db;

		public ExtraMenuItemController(IExtraMenuItemRepository dbExtraItem, IMapper mapper, ApplicationDbContext db)
		{
			_dbExtraItem = dbExtraItem;
			_mapper = mapper;
			_db = db;
			this._response = new();
		}

		[HttpGet]
		//[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetExtraMenuItems()
		{
			try
			{
				IEnumerable<ExtraMenuItem> ExtraMenuItemList = await _dbExtraItem.GetAllAsync();
				_response.Result = _mapper.Map<List<ExtraMenuItemDTO>>(ExtraMenuItemList);
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

		[HttpGet("{id:int}", Name = "GetExtraMemuItem")]
		//[Authorize]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> GetExtraMenuItem(int id)
		{
			try
			{
				if (id == 0)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}

				var ExtraMenuItem = await _dbExtraItem.GetAsync(u => u.ExtraMenuItemId == id);
				if (ExtraMenuItem == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					_response.IsSuccess = false;
					return NotFound(_response);
				}

				_response.Result = _mapper.Map<ExtraMenuItemDTO>(ExtraMenuItem);
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
		//[Authorize(Roles = "Chef")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> CreateExtraMenuItem([FromBody] ExtraMenuItemCreateDTO ExtraItemCreate)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				if (ExtraItemCreate == null)
				{
					_response.IsSuccess = false;
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}

				if (await _db.Chefs.FirstOrDefaultAsync(c => c.ChefId == ExtraItemCreate.ChefId) == null)
				{
					ModelState.AddModelError("Custom-Error", "Chef is not found");
					return BadRequest(ModelState);
				}

				ExtraMenuItem ExtraMenuItem = _mapper.Map<ExtraMenuItem>(ExtraItemCreate);
				await _dbExtraItem.CreateAsync(ExtraMenuItem);

				_response.Result = _mapper.Map<ExtraMenuItemDTO>(ExtraMenuItem);
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetExtraMemuItem", new { id = ExtraMenuItem.ExtraMenuItemId }, _response);
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
		//[Authorize(Roles ="CUSTOM")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> DeleteExtraMenuItem(int id)
		{
			try
			{
				if (id == 0)
				{
					_response.IsSuccess = false;
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}

				ExtraMenuItem ExtraMenuItem = await _dbExtraItem.GetAsync(e => e.ExtraMenuItemId == id);
				if (ExtraMenuItem == null)
				{
					_response.IsSuccess = false;
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}

				await _dbExtraItem.RemoveAsync(ExtraMenuItem);
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

		[HttpPut]
		//[Authorize(Roles = "Chef")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> UpdateExtraMenuItem(int id, [FromBody] ExtraMenuItemUpdateDTO ExtraItemUpdate)
		{
			try
			{
				if (id != ExtraItemUpdate.ExtraMenuItemId || ExtraItemUpdate == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}

				ExtraMenuItem ExtraMenuItem = _mapper.Map<ExtraMenuItem>(ExtraItemUpdate);
				await _dbExtraItem.UpdateAsync(ExtraMenuItem);

				_response.Result = _mapper.Map<ExtraMenuItemUpdateDTO>(ExtraMenuItem);
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