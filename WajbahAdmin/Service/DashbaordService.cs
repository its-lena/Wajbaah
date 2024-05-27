using Wajbah_API.Data;

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
    }
    public class DoughnutChartData
    {
        public string State { get; set; }
        public int Count { get; set; }
        public string FormattedCount { get; set; }
    }
}
