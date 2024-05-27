namespace WajbahAdmin.Service
{
    public interface IDashBoardService
    {
        int GetChefsCountByActiveStatus(bool active);
        List<DoughnutChartData> GetChefsDoughnutChartData();
        List<DoughnutChartData> GetCustomerDoughnutChartData();
    }
}
