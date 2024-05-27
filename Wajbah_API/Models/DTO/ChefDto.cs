using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Wajbah_API.Models.DTO
{
    public class ChefDto
    {
        public string ChefId { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required, MaxLength(50)]
        public string ChefFirstName { get; set; }
        [Required, MaxLength(50)]
        public string ChefLastName { get; set; }
        [Required, MaxLength(50)]
        public string RestaurantName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [NotNull]
        public string Description { get; set; }
        public double Rating { get; set; }
        public decimal Wallet { get; set; }
        [Required]
        public string ProfilePicture { get; set; }
        [Required]
        public Address Address { get; set; }
        public bool Active { get; set; }
        public bool State { get; set; }
        //public List<ExtraMenuItem> ExtraMenuItems { get; set; }
        public List<Menu_ItemDTO> MenuItems { get; set; }
        public ICollection<ChefPromoCode> PromoCodes { get; set; }
    }
}

