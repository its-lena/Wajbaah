using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Wajbah_API.Data;
using Wajbah_API.Models;
using Wajbah_API.Models.DTO;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Controllers
{
    [Authorize]
	[ApiController]
	[Route("api/OrderAPI")]
	public class OrderAPIController : ControllerBase
	{
        private readonly IOrderRepository _dbItem;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        protected readonly IMenuItemRepository _dbMenuItem;
        protected readonly IPromoCodeRepository _dbPromoCode ;
        //will be replaced with needed IRepositories
        private readonly ApplicationDbContext _db;
        public OrderAPIController(ApplicationDbContext db, IOrderRepository dbItem, IMapper mapper, IMenuItemRepository dbMenuItem, IPromoCodeRepository dbPromoCode)
        {
            _dbItem = dbItem;
            _mapper = mapper;
            _dbMenuItem = dbMenuItem;
            this._response = new();
            _db = db;
            _dbPromoCode = dbPromoCode;
        }

        [HttpGet]
		//[Authorize(Roles = "admin")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]

		public async Task<ActionResult<APIResponse>> GetOrders()
        {
            try
            {
                IEnumerable<Order> Orders = await _dbItem.GetAllAsync();
                _response.Result = _mapper.Map<List<OrderDTO>>(Orders);
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

        [HttpGet("{id:int}", Name ="GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		//[Authorize]
		public async Task<ActionResult<APIResponse>> GETOrder(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                Order Order = await _dbItem.GetAsync(u => u.OrderId == id);
                if (Order == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound; 
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<OrderDTO>(Order);
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
		//[Authorize]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<APIResponse>> CreateOrder([FromBody] OrderCreateDTO orderCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
					ModelState.AddModelError("Custom-Error", "Something wrong with the model");
					return BadRequest(ModelState);
                }
                if(orderCreate == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if(await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == orderCreate.CustomerId) == null)
                {
                    ModelState.AddModelError("Custom-Error", "Customer ID is not found");
                    return BadRequest(ModelState);
                }
                List<MenuItem> menuItems = await _dbMenuItem.GetAllAsync(m => orderCreate.MenuItemIds.Contains(m.MenuItemId));

                Order order = _mapper.Map<Order>(orderCreate);

                order.MenuItems = menuItems;
                PromoCode promoCode = null;
                // Check if the promo code exists
                if (orderCreate.PromoCodeId.HasValue)
                {
                    promoCode = await _dbPromoCode.GetAsync(p=>p.PromoCodeId==orderCreate.PromoCodeId);
                    if (promoCode == null)
                    {
                        ModelState.AddModelError("Custom-Error","PromoCode not found");
                        return BadRequest(ModelState);
                    }
                }
                Company company = null;
                // Check if the company exists
                    company = await _db.Companies.FindAsync(orderCreate.CompanyId);
                if (company == null)
                    {
                        ModelState.AddModelError("Custom-Error","company not found");
                        return BadRequest(ModelState);
                    }
                
                order.PromoCode = promoCode;
                order.Company = company;
                await _dbItem.CreateAsync(order);

                _response.Result = _mapper.Map<OrderDTO>(order);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetOrder", new { id = order.OrderId }, _response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages =
                    new List<string>() { ex.Message };
            }
            return _response;
        }
    }
}
