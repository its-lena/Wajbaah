namespace Wajbah_API.Models
{
	public class ChefPromoCode
	{
        public int PromoCodeId { get; set; }
        public PromoCode PromoCode { get; set; }
        public string ChefId { get; set; }
        public Chef Chef { get; set; }
    }
}
