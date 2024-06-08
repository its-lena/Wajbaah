using Wajbah_API.Models;

namespace WajbahAdmin.Service
{
    public interface IDashBoardService
    {
        int GetChefsCountByActiveStatus(bool active);
        int GetOrdersCount();
        int GetCustmersCount();
        Task<IEnumerable<Order>> GetRecentTransactions();
        IEnumerable<SplineChartData> GetSplineChartDataAsync();
        List<DoughnutChartData> GetChefsDoughnutChartData();
        List<DoughnutChartData> GetCustomerDoughnutChartData();
    }
}
