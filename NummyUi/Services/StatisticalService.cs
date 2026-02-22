using NummyShared.DTOs.Domain;
using NummyUi.Services.Abstract;
using NummyUi.Utils;

namespace NummyUi.Services;

public class StatisticalService(IHttpClientFactory clientFactory) : IStatisticalService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyConstants.ClientName);

    public async Task<TotalCountsResponseDto> GetTotalCounts()
    {
        var response = await _client.GetAsync(NummyConstants.GetTotalCountsUrl);

        if (!response.IsSuccessStatusCode)
            return new TotalCountsResponseDto(0, 0, 0, [], 0, [], 0, 0, 0, 0);

        var result = await response.Content.ReadFromJsonAsync<TotalCountsResponseDto>();
        return result ?? new TotalCountsResponseDto(0, 0, 0, [], 0, [], 0, 0, 0, 0);
    }
}
