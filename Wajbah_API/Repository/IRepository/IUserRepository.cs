using Wajbah_API.Models;
using Wajbah_API.Models.DTO;

namespace Wajbah_API.Repository.IRepository
{
	public interface IUserRepository
	{
		bool IsUniqueUser(int PhoneNumber);
		Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
		Task<Customer> Register(RegistrationRequestDTO registrationRequestDTO);
	}
}
