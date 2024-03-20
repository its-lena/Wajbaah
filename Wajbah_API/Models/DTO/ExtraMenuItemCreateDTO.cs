﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Wajbah_API.Models.DTO
{
	public class ExtraMenuItemCreateDTO
	{
		public int ExtraMenuItemId { get; set; }
		[Required]
		public decimal Price { get; set; }
		[NotNull]
		public string Description { get; set; }
		[Required]
		public string Name { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ChefId { get; set; }
	}
}
