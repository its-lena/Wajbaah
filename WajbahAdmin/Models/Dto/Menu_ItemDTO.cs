using System.ComponentModel.DataAnnotations;

namespace WajbahAdmin.Models.Dto
{
    public class Menu_ItemDTO
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        [Required]
        public string EstimatedTime { get; set; }
        public string OrderingTime { get; set; }
        [Required]
        public string Category { get; set; }
        public string Occassions { get; set; }
        public bool HealthyMode { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; }
        public string Photo { get; set; }
        public string RestaurantPhoto { get; set; }
        public string RestaurantName { get; set; }

    }
}
