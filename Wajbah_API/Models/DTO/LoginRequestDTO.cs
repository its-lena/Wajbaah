using System.ComponentModel.DataAnnotations;

namespace Wajbah_API.Models.DTO
{
	public class LoginRequestDTO
	{
		public int PhoneNumber { get; set; }
		public string Password { get; set; }
	}
}
