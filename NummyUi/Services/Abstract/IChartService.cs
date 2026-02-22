using NummyUi.Models;

namespace NummyUi.Services.Abstract;

public interface IChartService
{
    Task<ChartDataItem[]> GetVisitDataAsync();
    Task<ChartDataItem[]> GetVisitData2Async();
    Task<ChartDataItem[]> GetSalesDataAsync();
    Task<RadarDataItem[]> GetRadarDataAsync();
}
