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

            ViewBag.OnlineChefs = onlineChefs.ToString("N0");
            ViewBag.OfflineChefs = offlineChefs.ToString("N0");
            //chef doughnut chart
            var chefdoughnutChartData = _ds.GetChefsDoughnutChartData();
            ViewBag.ChefdoughnutChartData = chefdoughnutChartData;

            //customer doughnut chart
            var customerdoughnutChartData = _ds.GetCustomerDoughnutChartData();
            ViewBag.CustomerdoughnutChartData = customerdoughnutChartData;
            return View();
        }
    }
}
