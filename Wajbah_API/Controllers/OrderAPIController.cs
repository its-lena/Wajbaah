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
		protected readonly IPromoCodeRepository _dbPromoCode;
		private readonly IChefRepository _dbChef;
		private readonly ICustomerRepository _dbCustomer;
		private readonly IOrderRepository _dbOrder;
		//will be replaced with needed IRepositories
		private readonly ApplicationDbContext _db;
		public OrderAPIController(ApplicationDbContext db, IOrderRepository dbItem, IMapper mapper, 
			IMenuItemRepository dbMenuItem, IPromoCodeRepository dbPromoCode, IChefRepository dbChef,
			ICustomerRepository dbCustomer, IOrderRepository dbOrder)
		{
			_dbItem = dbItem;
			_mapper = mapper;
			_dbMenuItem = dbMenuItem;
			this._response = new();
			_db = db;
			_dbPromoCode = dbPromoCode;
			_dbChef = dbChef;
			_dbCustomer = dbCustomer;
			_dbOrder = dbOrder;
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
				var orders = await _db.Orders
			.Include(o => o.OrderMenuItems)
				.ThenInclude(omi => omi.MenuItem)
			.ToListAsync();

				if (orders == null || !orders.Any())
				{
					_response.IsSuccess = false;
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}

				var orderDtos = orders.Select(order =>
				{
					var orderDto = _mapper.Map<OrderDTO>(order);
					orderDto.MenuItems = order.OrderMenuItems.Select(omi => _mapper.Map<Menu_ItemDTO>(omi.MenuItem)).ToList();
					orderDto.Quanitities = order.OrderMenuItems.Select(o => o.Quantity).ToList();
					orderDto.Sizes = order.OrderMenuItems.Select(o => o.Size).ToList();
					return orderDto;
				}).ToList();

				_response.Result = orderDtos;
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

		[HttpGet("{id:int}", Name = "GetOrder")]
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
				Order order = await _db.Orders
			.Include(o => o.OrderMenuItems)
				.ThenInclude(omi => omi.MenuItem)
			.FirstOrDefaultAsync(o => o.OrderId == id);
				

				if (order == null)
				{
					_response.IsSuccess = false;
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				var orderDto = _mapper.Map<OrderDTO>(order);
				orderDto.Quanitities = order.OrderMenuItems.Select(o => o.Quantity).ToList();
				orderDto.Sizes = order.OrderMenuItems.Select(o => o.Size).ToList();

				_response.Result = orderDto;
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
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<ActionResult<APIResponse>> CreateOrder([FromBody] OrderCreateDTO orderCreate)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					ModelState.AddModelError("Custom-Error", "Something wrong with the model");
					return BadRequest(ModelState);
				}
				if (orderCreate == null)
				{
					_response.IsSuccess = false;
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}

				// Validate Customer
				var customer = await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == orderCreate.CustomerId);
				if (customer == null)
				{
					ModelState.AddModelError("Custom-Error", "Customer ID is not found");
					return BadRequest(ModelState);
				}
			
				// Validate Chef
				var chef = await _dbChef.GetAsync(c => c.ChefId == orderCreate.ChefId);
				if (chef == null)
				{
					ModelState.AddModelError("Custom-Error", "Chef ID is not found");
					return BadRequest(ModelState);
				}

				// Validate and retrieve MenuItems
				var menuItems = await _dbMenuItem.GetAllAsync(m => orderCreate.MenuItemIds.Contains(m.MenuItemId));
				if (!menuItems.Any())
				{
					ModelState.AddModelError("Custom-Error", "No valid MenuItem IDs provided");
					return BadRequest(ModelState);
				}

				// Validate PromoCode if provided
				PromoCode promoCode = null;
				if (orderCreate.PromoCodeId.HasValue)
				{
					promoCode = await _dbPromoCode.GetAsync(p => p.PromoCodeId == orderCreate.PromoCodeId);
					if (promoCode == null)
					{
						ModelState.AddModelError("Custom-Error", "PromoCode not found");
						return BadRequest(ModelState);
					}
				}

				// Validate Company if provided
				Company company = null;
				if (orderCreate.CompanyId.HasValue)
				{
					company = await _db.Companies.FindAsync(orderCreate.CompanyId);
					if (company == null)
					{
						ModelState.AddModelError("Custom-Error", "Company not found");
						return BadRequest(ModelState);
					}
				}
                

                // Map the OrderCreateDTO to Order entity
                var order = _mapper.Map<Order>(orderCreate);

				//retrieve customer info
				var customerId = await _dbCustomer.GetAsync(c => c.CustomerId == orderCreate.CustomerId);
                if (customer == null)
                {
                    ModelState.AddModelError("Custom-Error", "Customer ID is not found");
                    return BadRequest(ModelState);
                }
				order.CustomerPhoneNumber = customerId.PhoneNumber;
				order.CustomerFirstName = customerId.FirstName;
				order.CustomerLastName = customerId.LastName;

                // Assign related entities
                order.Customer = customer;
				order.Chef = chef;
				order.PromoCode = promoCode;
				order.Company = company;
				order.MenuItems = menuItems;

				// Create OrderMenuItems
				order.OrderMenuItems = new List<OrderMenuItem>();
				for (int i= 0 ; i< menuItems.Count; i++)
				{
					var orderMenuItem = new OrderMenuItem
					{
						Order = order,
						MenuItem = menuItems[i],
						Quantity = orderCreate.Quanitities[i], 
						Size = orderCreate.Sizes[i] 
					};
					order.OrderMenuItems.Add(orderMenuItem);
				}

				// Create the Order
				await _dbItem.CreateAsync(order);

				_response.Result = _mapper.Map<OrderDTO>(order);
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetOrder", new { id = order.OrderId }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string> { ex.Message };
				return StatusCode(StatusCodes.Status500InternalServerError, _response);
			}
		}
        [HttpGet("GetOrdersRequests", Name = "GetOrdersRequests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> GetOrdersRequests(string chefId)
        {
            try
            {
                if (string.IsNullOrEmpty(chefId))
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var orders = await _dbItem.GetAllAsync(o => o.ChefId == chefId && o.Status=="Pending", includeProperties: x => x.MenuItems);
                if (orders == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }


                IEnumerable<OrderDTO> ordersDto = _mapper.Map<IEnumerable<OrderDTO>>(orders);

                _response.Result = ordersDto;

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
		
		[HttpPut("ChangeOrderState", Name = "ChangeOrderState")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIResponse>> ChangeOrderState([FromQuery]int orderId,[FromQuery] string status)
        {
            try
            {
                if (string.IsNullOrEmpty(status))
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var order = await _dbItem.GetAsync(o=>o.OrderId==orderId);
                if (order == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
					_response.ErrorMessages.Add("No order with this ID");
                    return NotFound(_response);
                }

				order.Status= status;
				await _dbItem.UpdateAsync(order);
                OrderDTO ordersDto = _mapper.Map<OrderDTO>(order);

                _response.Result = ordersDto;

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
		
		[HttpGet("OrderTracking", Name ="OrderTracking")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> OrderTracking(int orderId)
		{
			try
			{
				if (orderId == 0)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					_response.IsSuccess = false;
					return BadRequest(_response);
				}
				Order order = await _dbOrder.GetAsync(o => o.OrderId==orderId);

				if (order == null)
				{
					_response.Result = "Order not found";
					_response.IsSuccess = false;
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}

				_response.Result = order.Status;
				_response.StatusCode = HttpStatusCode.OK;
				return Ok(_response);
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
