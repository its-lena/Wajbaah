using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Wajbah_API.Models
{
	public class ExtraMenuItem
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ExtraMenuItemId { get; set; }
		[Required]
		public decimal Price { get; set; }
		[NotNull]
		public string Description { get; set; }
		[Required]
		public string Name { get; set; }
        public DateTime CreatedOn { get; set; }

        //chef-ExtraMenuItem relation (many to one)
        public string ChefId { get; set; }
        public Chef Chef { get; set; }
        //order-ExtraMenuItem relation (many to many)
        public ICollection<Order> Orders { get; set; }
        public List<OrderExtraMenuItem> OrderExtraMenuItems { get; set; }
    }
}
