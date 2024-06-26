﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wajbah_API.Models.DTO.PromoCode
{
    public class PromoCodeDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromoCodeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }
        [Required]
        public decimal DiscountPercentage { get; set; }
        [Required]
        public int MaxUsers { get; set; }
        [Required]
        public int MaxLimit { get; set; }
        [Required]
        public int MinLimit { get; set; }
		//public List<string> ChefIds { get; set; }

	}
}
