using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wajbah_API.Models;
using Wajbah_API.Models.DTO;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Controllers
{
	[ApiController]
	[Route("api/UserAuth")]
	public class UserController : Controller
	{
		private readonly IUserRepository _userRepo;
		protected APIResponse _response;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
			this._response = new();
        }

		[HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
		{
			var loginResponse = await _userRepo.Login(model);

			if(loginResponse.customer == null || string.IsNullOrEmpty(loginResponse.Token))
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorMessages.Add("Username or Password is incorrect");
				return BadRequest(_response);
			}
			if(loginResponse.customer.State == false )
			{
				_response.StatusCode = HttpStatusCode.Unauthorized;
				_response.IsSuccess = false;
				_response.ErrorMessages.Add("This User is suspended");
				return Unauthorized(_response);
			}

			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			_response.Result = loginResponse;
			return Ok(_response);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
		{
			bool isUserUnique = _userRepo.IsUniqueUser(model.PhoneNumber);

			if(!isUserUnique)
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorMessages.Add("Phone Number already exists!");
				return BadRequest(_response);
			}

			var cust = await _userRepo.Register(model);

			if( cust == null)
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.IsSuccess = false;
				_response.ErrorMessages.Add("Error while registration");
				return BadRequest(_response);
			}

			_response.StatusCode = HttpStatusCode.OK;
			_response.IsSuccess = true;
			return Ok(_response);
		}
	}
}
