using System.ComponentModel.DataAnnotations;

namespace Wajbah_API.Models.DTO
{
	public class RegistrationRequestDTO
	{
		public int PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Role { get; set; }
		public DateTime BirthDate { get; set; }
        public string? Favourites { get; set; }
        public string? UsedCoupones { get; set; }
	}
}
