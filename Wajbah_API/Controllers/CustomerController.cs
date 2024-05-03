using AutoMapper;
using Wajbah_API.Data;
using Microsoft.AspNetCore.Mvc;
using Wajbah_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Wajbah_API.Repository.IRepository;
using Wajbah_API.Models.DTO;


namespace Wajbah_API.Controllers
{
	[Route("api/Customer")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ICustomerRepository _dbCustomer;
		private readonly APIResponse _response;
		public CustomerController(ICustomerRepository dbCustomer, IMapper mapper)
		{
			_mapper = mapper;
			_dbCustomer = dbCustomer;
			this._response = new APIResponse();
		}

		[HttpPost(Name = "CreateCustomer")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> CreateCustomer([FromBody] CustomerCreateDto customerCreateDto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				if (await _dbCustomer.GetAsync(c => c.Email == customerCreateDto.Email.ToLower()) != null)
				{
					ModelState.AddModelError("ExstingError", "this Email registered before");
					return BadRequest(ModelState);
				}
				else if (await _dbCustomer.GetAsync(c => c.PhoneNumber == customerCreateDto.PhoneNumber) != null)
				{
					ModelState.AddModelError("ExstingError", "this phone number registered before");
					return BadRequest(ModelState);
				}
				else
				{
					Customer customer = _mapper.Map<Customer>(customerCreateDto);
					customer.Email = customerCreateDto.Email.ToLower();
					customer.Wallet = 0;
					customer.UsedCoupones = "";
					await _dbCustomer.CreateAsync(customer);
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
		[HttpGet("{id:int}", Name = "GetCustomer")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetCustomer(int id)
		{
			try
			{

				Customer model = await _dbCustomer.GetAsync(c => c.CustomerId == id);
				if (model == null)
				{
					ModelState.AddModelError("ExstingError", "There is no account with this Id");
					return NotFound(ModelState);
				}

				_response.StatusCode = HttpStatusCode.OK;
				CustomerDto customerGet = _mapper.Map<CustomerDto>(model);
				_response.Result = customerGet;
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

		[HttpPut("{id:int}", Name = "UpdateCustomer")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<APIResponse>> UpdateCustomer(int id, [FromBody] CustomerUpdateDto customerUpdate)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				Customer model = await _dbCustomer.GetAsync(c => c.CustomerId == id, false);
				if (model == null)
				{
					ModelState.AddModelError("ExstingError", "There is no account with this id");
					return NotFound(ModelState);
				}
				model = _mapper.Map<Customer>(customerUpdate);
				model.CustomerId = id;
				await _dbCustomer.UpdateAsync(model);
				_response.StatusCode = HttpStatusCode.OK;
				_response.Result = customerUpdate;
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

		[HttpDelete("{id:int}", Name = "DeleteCustomer")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> DeleteCustomer(int id)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				Customer model = await _dbCustomer.GetAsync(c => c.CustomerId == id, false);
				if (model == null)
				{
					ModelState.AddModelError("ExstingError", "There is no account with this id");
					return NotFound(ModelState);
				}
				await _dbCustomer.RemoveAsync(model);
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