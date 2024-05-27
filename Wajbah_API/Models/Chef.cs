using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Wajbah_API.Models
{
	public class Chef
	{
		//don't forget to name the colomn national id
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[StringLength(14)]
		public string ChefId { get; set; } //NationalID
		[Required]
		public int PhoneNumber { get; set; }
		[Required]
		public string Email { get; set; }
        public string Role { get; set; }
        [Required, MinLength(8)]
		public string Password { get; set; }
		[Required, MaxLength(25)]
		public string ChefFirstName { get; set; }
		[Required, MaxLength(25)]
		public string ChefLastName { get; set; }
		[Required, MaxLength(50)]
		public string RestaurantName { get; set; }
		[Required]
		public DateTime BirthDate { get; set; }
		[Required]
		public string Description { get; set; }
		//:(اعمليها انتي يا لينا
		public double Rating { get; set; } = 5.0;
		public decimal Wallet { get; set; }
        public bool State { get; set; } = false;
        [Required]
		public string ProfilePicture { get; set; }
		[Required]
		public Address Address { get; set; }
		public bool Active { get; set; }=false;

        //chef-ExtraMenuItem relation (many to one)
        public List<ExtraMenuItem> ExtraMenuItems { get; set; }
        //chef-MenuItem relation (many to one)
        public List<MenuItem> MenuItems { get; set; }
		
        //chef-PromoCode relation (many to many)
        public ICollection<PromoCode> PromoCodes { get; set; }
        public List<ChefPromoCode> ChefPromoCodes { get; set; }
		
    }
}
