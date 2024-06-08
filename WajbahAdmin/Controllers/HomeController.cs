using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WajbahAdmin.Models;
using WajbahAdmin.Service;

namespace WajbahAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashBoardService _ds;

        public HomeController(IDashBoardService ds)
        {
            _ds = ds;
        }

        public IActionResult Index()
        {
            int onlineChefs = _ds.GetChefsCountByActiveStatus(true);
            int offlineChefs = _ds.GetChefsCountByActiveStatus(false);
            int ordersNumber = _ds.GetOrdersCount();
            int customersNumber = _ds.GetCustmersCount();

            ViewBag.OnlineChefs = onlineChefs.ToString("N0");
            ViewBag.OfflineChefs = offlineChefs.ToString("N0");
            ViewBag.OrdersNumber = ordersNumber.ToString("N0");
            ViewBag.CustomersNumber = customersNumber.ToString("N0");
            var data= _ds.GetSplineChartDataAsync();
            ViewBag.SplineChartData= data;
            //var legendData= _ds.GetRecentTransactions();
            //ViewBag.RecentTransactions = data;

            var chefdoughnutChartData = _ds.GetChefsDoughnutChartData();
            ViewBag.ChefdoughnutChartData = chefdoughnutChartData;

            //customer doughnut chart
            var customerdoughnutChartData = _ds.GetCustomerDoughnutChartData();
            ViewBag.CustomerdoughnutChartData = customerdoughnutChartData;
            return View();
        }
    }
}
