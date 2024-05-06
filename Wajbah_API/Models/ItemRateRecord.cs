using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wajbah_API.Models
{
    public class ItemRateRecord
    {
        [Key, Column(Order = 0)]
        public int CustomerId { get; set; } // Foreign Key

        [Key, Column(Order = 1)]
        public int MenuItemId { get; set; } // Foreign Key

        [Range(0.1, 5.0)]
        public double Rating { get; set; } // Rating given by the customer

        // Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual MenuItem MenuItem { get; set; }
    }
}
