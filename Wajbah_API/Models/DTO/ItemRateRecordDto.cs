using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wajbah_API.Models.DTO
{
    public class ItemRateRecordDto
    {
        public int CustomerId { get; set; } // Foreign Key

        public int MenuItemId { get; set; } // Foreign Key

        [Range(0.1, 5.0)]
        public double Rating { get; set; } // Rating given by the customer

    }
}
