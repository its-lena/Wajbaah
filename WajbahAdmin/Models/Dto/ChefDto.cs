using Wajbah_API.Models;

namespace WajbahAdmin.Models.Dto
{
    public class ChefAdminDto
    {
        public string ChefId { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ChefFirstName { get; set; }
        public string ChefLastName { get; set; }
        public string RestaurantName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public decimal Wallet { get; set; }
        public string ProfilePicture { get; set; }
        public Address Address { get; set; }
        public bool Active { get; set; } 
        public bool State { get; set; }
        //public List<ExtraMenuItem> ExtraMenuItems { get; set; }
        public List<Menu_ItemDTO> MenuItems { get; set; }
        public ICollection<ChefPromoCode> PromoCodes { get; set; }
    }
}

