using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using Wajbah_API.Data;
using Wajbah_API.Models;
using Wajbah_API.Models.DTO;
using WajbahAdmin.Models.Dto;
using WajbahAdmin.Service;

namespace WajbahAdmin.Controllerss
{
    public class ChefHandelController : Controller
    {
        private IUserHandelService<Chef> _userHandel;
        private IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ChefHandelController(IUserHandelService<Chef> userHandel, IMapper mapper, ApplicationDbContext context)
        {
            _userHandel = userHandel;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet(Name = "SearchAsync")]
        public async Task<IActionResult> SearchAsync()
        {
            var result = await _userHandel.Search();
            if (result != null)
            {
                var orderedResults = result.OrderByDescending(c => c.ChefId).Take(7);
                List<ChefAdminDto> customers = _mapper.Map<List<ChefAdminDto>>(orderedResults);
                return View(customers);
            }
            return View();
        }

        [HttpPost(Name = "Search")]
        public async Task<IActionResult> Search(string searchString, bool active, bool notActive)
        {
            Expression<Func<Chef, bool>> filter;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (notActive)
                {
                    filter = p => (p.ChefFirstName.StartsWith(searchString) || p.ChefLastName.StartsWith(searchString)) && p.State == false;
                }
                else if (active)
                {
                    filter = p => (p.ChefFirstName.StartsWith(searchString) || p.ChefLastName.StartsWith(searchString)) && p.State == true;
                }
                else
                {
                    filter = p => p.ChefFirstName.StartsWith(searchString) || p.ChefLastName.StartsWith(searchString);
                }
            }
            else
            {
                if (notActive)
                {
                    filter = p => p.State == false;
                }
                else if (active)
                {
                    filter = p => p.State == true;
                }
                else
                {
                    filter = null;
                }
            }
            var result = await _userHandel.Search(filter);
            ViewData["query"] = searchString;
            ViewData["filterByName"] = false;
            ViewData["filterByStatus"] = false;
            if (result == null)
            {
                return View();
            }
            List<ChefAdminDto> chefs = _mapper.Map<List<ChefAdminDto>>(result);
            return View(chefs);
        }

        [HttpGet]
        public async Task<IActionResult> ChefDetails(string id)
        {
            ChefAdminDto chef = new ChefAdminDto();
          
            var result = await _userHandel.Get(c => c.ChefId == id);
            chef = _mapper.Map<ChefAdminDto>(result);

            return View(chef);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeState(string id)
        {
            var result = await _userHandel.Get(c => c.ChefId == id);
            if (result != null)
            {
                result.State = !result.State;
                await _userHandel.SaveAsync();
            }
            return RedirectToAction(nameof(Search));
        }

        [HttpGet("ChefShowOrders/{id}", Name = "ChefShowOrders")]
         public IActionResult ShowOrders(string id)
         {
            var orders = (from o in _context.Orders
                          where o.ChefId == id
                          select new OrderDto
                          {
                              OrderId = o.OrderId,
                              CustomerId=o.CustomerId,
                              ChefId=o.ChefId,
                              Status = o.Status,
                              MenuItems = (from omi in _context.OrderMenuItem
                                           where omi.OrderId == o.OrderId
                                           select omi.MenuItem.Name).ToList(),
                              TotalPrice = (from omi in _context.OrderMenuItem
                                            where omi.OrderId == o.OrderId
                                            select omi.Size.ToLower() == "small" ? omi.Quantity * omi.MenuItem.SizesPrices.PriceSmall :
                                                  omi.Size.ToLower() == "medium" ? omi.Quantity * omi.MenuItem.SizesPrices.PriceMedium :
                                                  omi.Quantity * omi.MenuItem.SizesPrices.PriceLarge).Sum()
                          }).ToList();


            return View(orders);
         }
    }
}
