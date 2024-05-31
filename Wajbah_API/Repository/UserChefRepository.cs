using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Wajbah_API.Data;
using Wajbah_API.Models.DTO;
using Wajbah_API.Models;
using Wajbah_API.Repository.IRepository;

namespace Wajbah_API.Repository
{
	public class UserChefRepository : IUserChefRepository
	{
		private readonly ApplicationDbContext _db;
		private string secretKey;
		public UserChefRepository(ApplicationDbContext db, IConfiguration configuration)
		{
			_db = db;
			secretKey = configuration.GetValue<string>("ApiSettings:Secret");

		}

		public bool IsUniqueUser(int PhoneNumber)
		{
			var user = _db.Chefs.FirstOrDefault(x => x.PhoneNumber == PhoneNumber);
			if (user == null)
			{
				return true;
			}
			return false;
		}

		public async Task<LoginResponseChefDTO> Login(LoginRequestChefDTO loginRequestChefDTO)
		{
			var cust = _db.Chefs.FirstOrDefault(c => c.PhoneNumber == loginRequestChefDTO.PhoneNumber &&
			c.Password == loginRequestChefDTO.Password);

			if (cust == null)
			{
				return new LoginResponseChefDTO()
				{
					chef = null,
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
					new Claim(ClaimTypes.Name, cust.ChefId.ToString()),
					new Claim(ClaimTypes.Role, cust.Role)
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			LoginResponseChefDTO loginResponseChefDTO = new LoginResponseChefDTO()
			{
				Token = tokenHandler.WriteToken(token),
				chef = cust
			};

			return loginResponseChefDTO;
		}

		public async Task<Chef> Register(RegistrationRequestChefDTO registrationRequestChefDTO)
		{
			Chef chef = new()
			{
				ChefId = registrationRequestChefDTO.ChefId,
				PhoneNumber = registrationRequestChefDTO.PhoneNumber,
				Email = registrationRequestChefDTO.Email,
				Password = registrationRequestChefDTO.Password,
				ChefFirstName = registrationRequestChefDTO.ChefFirstName,
				ChefLastName = registrationRequestChefDTO.ChefLastName,
				RestaurantName = registrationRequestChefDTO.RestaurantName,
				BirthDate = registrationRequestChefDTO.BirthDate,
				Description = registrationRequestChefDTO.Description,
				ProfilePicture = registrationRequestChefDTO.ProfilePicture,
				Address = registrationRequestChefDTO.Address
			};

			_db.Chefs.Add(chef);
			await _db.SaveChangesAsync();
			chef.Password = "";
			return chef;
		}
	}
}
