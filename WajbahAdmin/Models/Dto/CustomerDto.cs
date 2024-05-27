

namespace WajbahAdmin.Models.Dto
{
    public class CustomerDto
    {


        public int CustomerId { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Favourites { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Wallet { get; set; }
        public bool State { get; set; }
        //public string UsedCoupones { get; set; }
        //Customer-Order relation (many to one)
        public List<OrderDTO> Orders { get; set; }
    }
}
