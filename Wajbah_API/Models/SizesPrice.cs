namespace Wajbah_API.Models
{
	public class SizesPrice
	{
        //public string SizeSmall { get; set; }
        public decimal PriceSmall { get; set; }
		//public string SizeMedium { get; set; }
		public decimal PriceMedium { get; set; }
		//public string SizeLarge { get; set; }
		public decimal PriceLarge { get; set; }

		//MenuItem-SizePrice relation (many to one)
		/*public int MenuItemId { get; set; }*/
		//public MenuItem MenuItem { get; set; } cancelled
	}
}