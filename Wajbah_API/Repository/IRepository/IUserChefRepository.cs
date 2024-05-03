using Wajbah_API.Models.DTO;
using Wajbah_API.Models;

namespace Wajbah_API.Repository.IRepository
{
	public interface IUserChefRepository
	{
		bool IsUniqueUser(int PhoneNumber);
		Task<LoginResponseChefDTO> Login(LoginRequestChefDTO loginRequestChefDTO);
		Task<Chef> Register(RegistrationRequestChefDTO registrationRequestChefDTO);
	}
}
