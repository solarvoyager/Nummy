using System.Text.Json;
using NummyUi.Dtos.Domain;
using NummyUi.Utils;

namespace NummyUi.Services;

public interface IStatisticalService
{
    Task<TotalCountsResponseDto> GetTotalCounts();
}

public class StatisticalService(IHttpClientFactory clientFactory) : IStatisticalService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyContants.ClientName);

    public async Task<TotalCountsResponseDto> GetTotalCounts()
    {
        var response = await _client.GetFromJsonAsync<TotalCountsResponseDto>(NummyContants.GetTotalCountsUrl);

        return response!;
    }
}