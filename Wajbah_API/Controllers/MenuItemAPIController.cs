using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wajbah_API.Data;
using Wajbah_API.Models;
using Wajbah_API.Models.DTO;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Controllers
{
	
	[ApiController]
    [Route("api/MenuItemAPI")]
    public class MenuItemAPIController : ControllerBase
    {
        private readonly IMenuItemRepository _dbItem;
        private readonly IChefRepository _dbChef;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        //will be replaced
        private readonly ApplicationDbContext _db;
        private readonly IItemRateRepository _itemRateRepository;

        public MenuItemAPIController(IMenuItemRepository dbItem, IMapper mapper, ApplicationDbContext db, IChefRepository dbChef, IItemRateRepository itemRateRepository)
        {
            _dbItem = dbItem;
            _mapper = mapper;
            this._response = new();
            _dbChef = dbChef;
            _db = db;
            _itemRateRepository = itemRateRepository;
        }

        [Authorize]
        [HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<APIResponse>> GetMenuItems()
        {
            try
            {
                IEnumerable<MenuItem> MenuItemList = await _dbItem.GetAllAsync(includeProperties: x => x.Chef);

				var menuDtoList = MenuItemList.Select(menuItem => new Menu_ItemDTO
				{
					// Map properties from MenuItem to Menu_ItemDTO
					MenuItemId = menuItem.MenuItemId,
					Name = menuItem.Name,
					RestaurantName = menuItem.Chef?.RestaurantName,
                    EstimatedTime = menuItem.EstimatedTime,
                    OrderingTime = menuItem.OrderingTime,
                    Category = menuItem.Category,
                    Occassions = menuItem.Occassions,
                    SizesPrices = menuItem.SizesPrices,
                    HealthyMode = menuItem.HealthyMode,
                    Description = menuItem.Description,
                    Rate= menuItem.Rate,
                    Photo = menuItem.Photo,
                    RestaurantPhoto = menuItem.Chef?.ProfilePicture,
                    ChefId = menuItem.ChefId
				}).ToList();

                _response.Result = menuDtoList;
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

        [Authorize]
        [HttpGet("{id:int}", Name ="GetMenuItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<APIResponse>> GetMenuItem (int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var MenuItem = await _dbItem.GetAsync(u => u.MenuItemId == id, includeProperties: x => x.Chef);
                if (MenuItem == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }


                Menu_ItemDTO menuItemDto = _mapper.Map<Menu_ItemDTO>(MenuItem);
                menuItemDto.RestaurantName = MenuItem.Chef.RestaurantName;
                menuItemDto.RestaurantPhoto = MenuItem.Chef.ProfilePicture;

                _response.Result = menuItemDto;

				_response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages=
                    new List<string>() { ex.Message };
            }
            return _response;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<APIResponse>> CreateMenuItem([FromBody] Menu_ItemCreateDTO menuItemCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //if (await _dbItem.GetAsync(u => u.Name.ToLower() == menuItemCreate.Name.ToLower()) != null)
                //{
                //    ModelState.AddModelError("Custom-Error", "Item Name already exsists");
                //    return BadRequest(ModelState);
                //}

                if (menuItemCreate == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                /////////////////////////////////////////////////////////////////////////////////////////
                if(await _db.Chefs.FirstOrDefaultAsync(c => c.ChefId == menuItemCreate.ChefId) == null)
                {
					ModelState.AddModelError("Custom-Error", "chef is not found");
					return BadRequest(ModelState);
				}

                MenuItem menuItem = _mapper.Map<MenuItem>(menuItemCreate);
                await _dbItem.CreateAsync(menuItem);

                _response.Result = _mapper.Map<Menu_ItemDTO>(menuItem);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetMenuItem", new { id = menuItem.MenuItemId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.Message };
            }
            return _response;
        }

        [Authorize]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<APIResponse>> DeleteMenuItem(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                MenuItem menuItem = await _dbItem.GetAsync(u => u.MenuItemId == id);
                if (menuItem == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _dbItem.RemoveAsync(menuItem);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages= 
                    new List<string>() { ex.Message };
            }
            return _response;
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<APIResponse>> UpdateMenuItem (int id, [FromBody]Menu_ItemUpdateDTO menuItemUpdate)
        {
            try
            {
                if (id != menuItemUpdate.MenuItemId || menuItemUpdate == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                MenuItem menuItem = _mapper.Map<MenuItem>(menuItemUpdate);
                await _dbItem.UpdateAsync(menuItem);

                _response.Result = _mapper.Map<Menu_ItemUpdateDTO>(menuItem);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages=
                    new List<string>() { ex.Message };
            }
            return _response;
        }

        [Authorize]
        [HttpPost("UpdateItemRate", Name = "UpdateItemRate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateItemRate([FromBody] ItemRateRecordDto dto)
        {
            try
            {
                if (dto.MenuItemId == 0 || dto.CustomerId==0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var MenuItem = await _dbItem.GetAsync(u => u.MenuItemId == dto.MenuItemId, includeProperties: x => x.Chef);
                if (MenuItem == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }


                MenuItem.Rate = _dbItem.UpdateRate(dto.Rating, MenuItem.Rate);
                await _dbItem.UpdateAsync(MenuItem);
                _response.Result =await _itemRateRepository.SetRate(dto);
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
        
        [HttpGet("GetAllRatings",Name = "GetAllRatings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllRatings()
        {
            try
            {
                _response.Result = await _itemRateRepository.GetAllRates();
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
