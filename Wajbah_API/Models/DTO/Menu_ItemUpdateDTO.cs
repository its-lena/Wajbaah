using System.ComponentModel.DataAnnotations;

namespace Wajbah_API.Models.DTO
{
    public class Menu_ItemUpdateDTO
    {
        [Key]
        public int MenuItemId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string EstimatedTime { get; set; }
        public string OrderingTime { get; set; }
        [Required]
        public string Category { get; set; }
        public string Occassions { get; set; }
        [Required]
        public SizesPrice SizesPrices { get; set; }
        [Required]
        public bool HealthyMode { get; set; }
        [Required]
        public string Description { get; set; }
        public double Rate { get; set; } 
        [Required]
        public string Photo { get; set; }
		public string RestaurantPhoto { get; set; }
		[Required]
        public string ChefId { get; set; }

    }
}
