namespace Wajbah_API.Models
{
	public class SizePrice
	{
        public string Size { get; set; }
        public decimal Price { get; set; }

        //MenuItem-SizePrice relation (many to one)
        /*public int MenuItemId { get; set; }*/
        //public MenuItem MenuItem { get; set; } cancelled
    }
}