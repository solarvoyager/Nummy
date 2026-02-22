using NummyShared.DTOs.Domain;
using NummyUi.Services.Abstract;
using NummyUi.Utils;

namespace NummyUi.Services;

public class HelperService(IHttpClientFactory clientFactory) : IHelperService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyConstants.ClientName);

    public async Task<ServiceUrlResponseDto> GetServiceUrl()
    {
        var response = await _client.GetAsync(NummyConstants.GetServiceUrlUrl);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ServiceUrlResponseDto>();
        return result ?? throw new InvalidOperationException("Service URL response was empty.");
    }
}
