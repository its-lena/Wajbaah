using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Wajbah_API.Models.DTO
{
    public class ChefCreateDto
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
        public decimal Wallet { get; set; } = 0;
        [Required]
        public string ProfilePicture { get; set; }
        [Required]
        public Address Address { get; set; }
    }
}
