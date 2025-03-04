using System.Text.Json;
using NummyUi.Dtos.Domain;
using NummyUi.Utils;

namespace NummyUi.Services;

public interface IHelperService
{
    Task<DsnResponseDto> GetDsn();
}

public class HelperService(IHttpClientFactory clientFactory) : IHelperService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyContants.ClientName);

    public async Task<DsnResponseDto> GetDsn()
    {
        var response = await _client.GetFromJsonAsync<DsnResponseDto>(NummyContants.GetDsnUrl);

        return response!;
    }
}