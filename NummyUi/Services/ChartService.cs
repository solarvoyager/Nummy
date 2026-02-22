using NummyUi.Models;
using NummyUi.Services.Abstract;

namespace NummyUi.Services;

public class ChartService(HttpClient httpClient) : IChartService
{
    public async Task<ChartDataItem[]> GetVisitDataAsync()
    {
        return (await GetChartDataAsync()).VisitData;
    }

    public async Task<ChartDataItem[]> GetVisitData2Async()
    {
        return (await GetChartDataAsync()).VisitData2;
    }

    public async Task<ChartDataItem[]> GetSalesDataAsync()
    {
        return (await GetChartDataAsync()).SalesData;
    }

    public async Task<RadarDataItem[]> GetRadarDataAsync()
    {
        return (await GetChartDataAsync()).RadarData;
    }

    private async Task<ChartData> GetChartDataAsync()
    {
        var result = await httpClient.GetFromJsonAsync<ChartData>("data/fake_chart_data.json");
        return result ?? new ChartData();
    }
}
