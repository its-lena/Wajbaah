using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wajbah_API.Data;
using Wajbah_API.Models;
using Wajbah_API.Models.DTO;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _db;
		private string secretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
			secretKey = configuration.GetValue<string>("ApiSettings:Secret");

        }

		public bool IsUniqueUser(int PhoneNumber)
		{
			var user = _db.Customers.FirstOrDefault(x => x.PhoneNumber == PhoneNumber);
			if( user == null)
			{
				return true;
			}
			return false;
		}

		public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
		{
			var cust = _db.Customers.FirstOrDefault(c => c.PhoneNumber == loginRequestDTO.PhoneNumber && 
			c.Password == loginRequestDTO.Password);

			if(cust == null)
			{
				return new LoginResponseDTO()
				{
					customer = null,
					Token = ""
				};
			}

			// if customer data exists
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secretKey);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, cust.CustomerId.ToString()),
					new Claim(ClaimTypes.Role, cust.Role)
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
			{
				Token = tokenHandler.WriteToken(token),
				customer = cust
			};

			return loginResponseDTO;
		}

		public async Task<Customer> Register(RegistrationRequestDTO registrationRequestDTO)
		{
			Customer customer = new()
			{
				FirstName = registrationRequestDTO.FirstName,
				LastName = registrationRequestDTO.LastName,
				PhoneNumber = registrationRequestDTO.PhoneNumber,
				BirthDate = registrationRequestDTO.BirthDate,
				Email = registrationRequestDTO.Email,
				Password = registrationRequestDTO.Password,
				Role = registrationRequestDTO.Role,
				UsedCoupones = registrationRequestDTO.UsedCoupones,
				Favourites = registrationRequestDTO.Favourites
			};

			_db.Customers.Add(customer);
			await _db.SaveChangesAsync();
			customer.Password = "";
			return customer;
		}
	}
}
