using NummyShared.DTOs;
using NummyUi.Services.Abstract;
using NummyUi.Utils;

namespace NummyUi.Services;

public class ApplicationService(IHttpClientFactory clientFactory) : IApplicationService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyConstants.ClientName);

    public async Task<ApplicationToListDto?> Get(Guid id)
    {
        var response = await _client.GetAsync(NummyConstants.GetApplicationsUrl + $"/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ApplicationToListDto>();
    }

    public async Task<IEnumerable<ApplicationToListDto>> Get()
    {
        var response = await _client.GetAsync(NummyConstants.GetApplicationsUrl);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<ApplicationToListDto>>();
        return result ?? [];
    }

    public async Task<IEnumerable<ApplicationStackToListDto>> GetStackType()
    {
        var response = await _client.GetAsync(NummyConstants.GetApplicationStackTypesUrl);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<ApplicationStackToListDto>>();
        return result ?? [];
    }

    public async Task<ApplicationToListDto> Add(string name, string description, string? healthCheckerUrl, Guid stackTypeId)
    {
        var response = await _client.PostAsJsonAsync(NummyConstants.AddApplicationUrl,
            new ApplicationToAddDto(name, description, healthCheckerUrl, stackTypeId));

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ApplicationToListDto>();
        return result ?? throw new InvalidOperationException("Add application response was empty.");
    }

    public async Task<ApplicationToListDto?> Update(Guid id, string name, string description, string? healthCheckerUrl, Guid stackTypeId)
    {
        var response = await _client.PutAsJsonAsync(NummyConstants.UpdateApplicationUrl + $"/{id}",
            new ApplicationToUpdateDto(name, description, healthCheckerUrl, stackTypeId));

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ApplicationToListDto>();
    }

    public async Task Delete(Guid id)
    {
        var response = await _client.DeleteAsync(NummyConstants.DeleteApplicationUrl + $"/{id}");
        response.EnsureSuccessStatusCode();
    }
}
