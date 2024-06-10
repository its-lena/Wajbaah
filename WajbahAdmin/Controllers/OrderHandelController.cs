using Microsoft.AspNetCore.Mvc;
using Wajbah_API.Data;
using Wajbah_API.Models;
using WajbahAdmin.Models.Dto;

namespace WajbahAdmin.Controllers
{
    public class OrderHandelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderHandelController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = (from o in _context.Orders
                          select new OrderDto
                          {
                              OrderId = o.OrderId,
                              CustomerId = o.CustomerId,
                              ChefId = o.ChefId,
                              Status = o.Status,
                              MenuItems = (from omi in _context.OrderMenuItem
                                           where omi.OrderId == o.OrderId
                                           select omi.MenuItem.Name).ToList(),
                              TotalPrice = (from omi in _context.OrderMenuItem
                                            where omi.OrderId == o.OrderId
                                            select omi.Size.ToLower() == "small" ? omi.Quantity * omi.MenuItem.SizesPrices.PriceSmall :
                                                  omi.Size.ToLower() == "medium" ? omi.Quantity * omi.MenuItem.SizesPrices.PriceMedium :
                                                  omi.Quantity * omi.MenuItem.SizesPrices.PriceLarge).Sum()
                          }).Take(6).ToList();


            return View(orders);
        }
        public IActionResult Details(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
