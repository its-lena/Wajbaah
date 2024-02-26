using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Wajbah_API.Models.DTO
{
    public class ChefUpdateDto
    {

        [Required]
        [StringLength(14)]
        public string ChefId { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MaxLength(50)]
        public string ChefFirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string ChefLastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string RestaurantName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [NotNull]
        public string Description { get; set; }
        [Required]
        public string ProfilePicture { get; set; }
        [Required]
        public Address Address { get; set; }
    }

}
