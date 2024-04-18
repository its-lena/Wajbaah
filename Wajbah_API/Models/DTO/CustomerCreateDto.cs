using System.ComponentModel.DataAnnotations;

namespace Wajbah_API.Models.DTO
{
    public class CustomerCreateDto
    {
        [Required]
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
        [Required, MaxLength(25)]
        public string FirstName { get; set; }
        [Required, MaxLength(25)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public decimal Wallet { get; set; } = 0;
    }
}
