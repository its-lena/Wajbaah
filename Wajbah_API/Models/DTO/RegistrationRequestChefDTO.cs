using System.ComponentModel.DataAnnotations;

namespace Wajbah_API.Models.DTO
{
	public class RegistrationRequestChefDTO
	{
		public string ChefId { get; set; } //NationalID
		public int PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ChefFirstName { get; set; }
		public string ChefLastName { get; set; }
		public string RestaurantName { get; set; }
		public DateTime BirthDate { get; set; }
		public string Description { get; set; }
		public string ProfilePicture { get; set; }
		public Address Address { get; set; }
	}
}
