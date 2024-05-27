using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wajbah_API.Models
{
	public class MenuItem
	{
		[Key]
		//[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MenuItemId { get; set; }
		[Required, MaxLength(50)]
		public string Name { get; set; }
		[Required]
		public string Category { get; set; }
		public string Occassions { get; set; }
		public string EstimatedTime { get; set; }
		public string OrderingTime { get; set; }
		public bool HealthyMode { get; set; }
		[Required]
		public string Description { get; set; }
        public double Rate { get; set; } = 5.0;
        [Required]
		public string Photo { get; set; }
        public string RestaurantPhoto { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
		//chef-MenuItem relation (many to one)
		[Required]
        public string ChefId { get; set; }
        public Chef Chef { get; set; }
        //order-MenuItem relation (many to many)
        public ICollection<Order> Orders { get; set; }
        public List<OrderMenuItem> OrderMenuItems { get; set; }

		//MenuItem-SizePrice relation (many to one)
		[Required]
        public SizesPrice SizesPrices { get; set; }
    }
}
