using NummyShared.Dtos.Domain;
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
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(NummyContants.GetTotalCountsUrl, UriKind.Relative)
        };

        var response = await _client.SendAsync(request);

        // TODO: Check if the response is successful
        // show message to user
        if (!response.IsSuccessStatusCode)
        {
            return new TotalCountsResponseDto(0, 0, 0, [], 0, [], 0, 0, 0, 0);
        }

        var result = await response.Content.ReadFromJsonAsync<TotalCountsResponseDto>();

        return result!;
    }
}