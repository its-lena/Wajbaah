using AutoMapper;
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
        //هشيلها قدام
        private readonly ApplicationDbContext _db;

        public MenuItemAPIController(IMenuItemRepository dbItem, IMapper mapper, ApplicationDbContext db)
        {
            _dbItem = dbItem;
            _mapper = mapper;
            this._response = new();
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetMenuItems()
        {
            try
            {
                IEnumerable<MenuItem> MenuItemList = await _dbItem.GetAllAsync();
                _response.Result = _mapper.Map<List<Menu_ItemDTO>>(MenuItemList);
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

        [HttpGet("{id:int}", Name ="GetMenuItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                var MenuItem = _dbItem.GetAsync(u => u.MenuItemId == id);
                if (MenuItem == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                Chef chef = await _dbChef.GetAsync(c => c.ChefId == MenuItem.ChefId);
                var model= _mapper.Map<Menu_ItemDTO>(MenuItem);
                model.RestaurantName=chef.RestaurantName;
                model.ProfilePicture=chef.ProfilePicture;
                _response.Result=model;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
    }
}
