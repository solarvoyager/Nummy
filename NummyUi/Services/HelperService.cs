using NummyShared.DTOs.Domain;
using NummyUi.Utils;

namespace NummyUi.Services;

public interface IHelperService
{
    Task<ServiceUrlResponseDto> GetServiceUrl();
}

public class HelperService(IHttpClientFactory clientFactory) : IHelperService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyConstants.ClientName);

    public async Task<ServiceUrlResponseDto> GetServiceUrl()
    {
        var response = await _client.GetFromJsonAsync<ServiceUrlResponseDto>(NummyConstants.GetServiceUrlUrl);

        return response!;
    }
}