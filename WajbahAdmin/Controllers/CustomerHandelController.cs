using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using Wajbah_API.Data;
using Wajbah_API.Models;
using WajbahAdmin.Models.Dto;
using WajbahAdmin.Service;

namespace WajbahAdmin.Controllerss
{
    public class CustomerHandelController : Controller
    {
        private IUserHandelService<Customer> _userHandel;
        private IMapper _mapper;
        private readonly ApplicationDbContext _context;


        public CustomerHandelController(IUserHandelService<Customer> userHandel, IMapper mapper,ApplicationDbContext context )
        {
            _userHandel= userHandel;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> SearchAsync()
        { 
            var result = await _userHandel.Search();
            if (result != null)
            {
                var orderedResults = result.OrderByDescending(c => c.CustomerId).Take(7);
                List<CustomerDto> customers = _mapper.Map<List<CustomerDto>>(orderedResults);
                return View(customers);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchString, bool active, bool notActive)
        {
            Expression<Func<Customer, bool>> filter;
            if (!string.IsNullOrEmpty(searchString))
            {
                if (notActive)
                {
                    filter = p => (p.FirstName.StartsWith(searchString) || p.LastName.StartsWith(searchString)) && p.State == false;
                }
                else if (active)
                {
                    filter = p => (p.FirstName.StartsWith(searchString) || p.LastName.StartsWith(searchString)) && p.State == true;
                }
                else
                {
                    filter = p => p.FirstName.StartsWith(searchString) || p.LastName.StartsWith(searchString);
                }
            }
            else
            {
                if (notActive)
                {
                    filter = p =>  p.State == false;
                }
                else if (active)
                {
                    filter = p =>  p.State == true;
                }
                else
                {
                    filter = null;
                }
            }
            var result =await _userHandel.Search(filter);
            ViewData["query"] = searchString;
            ViewData["filterByName"] = false;
            ViewData["filterByStatus"] = false;
            if (result == null)
            {
                return View();
            }
            List<CustomerDto> customers= _mapper.Map<List<CustomerDto>>(result);
            return View(customers);
        }

        [HttpGet("CustomerDetails/{id:int}", Name = "CustomerDetails")]
        public async Task<IActionResult> CustomerDetails(int id)
        {
            CustomerDto customer=new CustomerDto();
            if (id > 0)
            {
                var result = await _userHandel.Get(c => c.CustomerId == id);
                customer = _mapper.Map<CustomerDto>(result);
            }
           
            return View(customer);
        } 
        
        [HttpPost("{id:int}")]
        public async Task<IActionResult> ChangeState(int id)
        {
            var result = await _userHandel.Get(c => c.CustomerId == id);
            if (result != null)
            {
                result.State = !result.State;
                await _userHandel.SaveAsync();
            }
            return RedirectToAction(nameof(Search));
        }

        [HttpGet("CustomerShowOrders/{id:int}", Name = "CustomerShowOrders")]
        public  IActionResult ShowOrders(int id)
        {
            var orders = (from o in _context.Orders
                          where o.CustomerId == id
                          select new OrderDto
                          {
                              OrderId = o.OrderId,
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
                          }).ToList();
            return View(orders);
        }
    }
}
