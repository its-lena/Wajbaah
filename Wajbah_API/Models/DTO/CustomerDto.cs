using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wajbah_API.Models.DTO
{
    public class CustomerDto
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        public string Email { get; set; }

        [Required, MaxLength(25)]
        public string FirstName { get; set; }
        [Required, MaxLength(25)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public decimal Wallet { get; set; }
        public string UsedCoupones { get; set; }
        //Customer-Order relation (many to one)
        public List<Order> Orders { get; set; }
    }
}
