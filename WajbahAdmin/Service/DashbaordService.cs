using Microsoft.EntityFrameworkCore;
using System.Web.WebPages;
using Wajbah_API.Data;
using Wajbah_API.Models;

namespace WajbahAdmin.Service
{
    public class DashbaordService: IDashBoardService
    {
        private readonly ApplicationDbContext _dbContext;

        public DashbaordService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int GetChefsCountByActiveStatus(bool active)
        {
            var count = _dbContext.Chefs.Count(chef => chef.Active == active);
            return count;
        }
        public int GetOrdersCount()
        {
            var count = _dbContext.Orders.Count();
            return count;
        }
        public int GetCustmersCount()
        {
            var count = _dbContext.Customers.Count();
            return count;
        }

        public List<DoughnutChartData> GetChefsDoughnutChartData()
        {
            var chefsData = _dbContext.Chefs
                .GroupBy(chef => chef.State)
                .Select(group => new DoughnutChartData
                {
                    State = group.Key ? "Valid" : "Suspended",
                    Count = group.Count(),
                    FormattedCount = group.Count().ToString("N0")
                })
                .ToList();

            return chefsData;
        }
        public List<DoughnutChartData> GetCustomerDoughnutChartData()
        {
            var customersData = _dbContext.Customers
                .GroupBy(customer => customer.State)
                .Select(group => new DoughnutChartData
                {
                    State = group.Key ? "Valid" : "Suspended",
                    Count = group.Count(),
                    FormattedCount = group.Count().ToString("N0")
                })
                .ToList();

            return customersData;
        }
        public IEnumerable<SplineChartData> GetSplineChartDataAsync()
        {
            DateTime StartDate = DateTime.Today.AddDays(-29);
            DateTime EndDate = DateTime.Today;
            List<SplineChartData> ordersSummary = _dbContext.Orders
               .GroupBy(o => o.CreatedOn)
               .Select(x => new SplineChartData()
               {
                   day = x.First().CreatedOn.ToString("dd-MMM"),
                   income = (int)x.Sum(o=>o.TotalPrice)
               })
               .ToList();
            string[] Last30Days = Enumerable.Range(0, 30)
                .Select(y => StartDate.AddDays(y).ToString("dd-MMM"))
                .ToArray();
            var data= from day in Last30Days
                      join income in ordersSummary on day equals income.day into dayIncomeJoined
                      from income in dayIncomeJoined.DefaultIfEmpty()
                      select new SplineChartData()
                      {
                          day =day,
                          income = income == null ? 0 : income.income,
                      };
            return data;
        }
        public async Task<IEnumerable<Order>> GetRecentTransactions()
        {
            var data= await _dbContext.Orders
                .Include(o => o.Chef.RestaurantName)
                .OrderByDescending(j => j.CreatedOn)
                .Take(5)
                .ToListAsync();
            return data;
        }
    }
    public class DoughnutChartData
    {
        public string State { get; set; }
        public int Count { get; set; }
        public string FormattedCount { get; set; }
    }
    public class SplineChartData
    {
        public string day;
        public int income;
    }
}
