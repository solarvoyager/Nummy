using NummyShared.DTOs;
using NummyUi.Services.Abstract;
using NummyUi.Utils;

namespace NummyUi.Services;

public class TeamService(IHttpClientFactory clientFactory) : ITeamService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyConstants.ClientName);

    public async Task<TeamToListDto?> Get(Guid id)
    {
        var response = await _client.GetAsync(NummyConstants.GetTeamsUrl + $"/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<TeamToListDto>();
    }

    public async Task<IEnumerable<TeamToListDto>> Get()
    {
        var response = await _client.GetAsync(NummyConstants.GetTeamsUrl);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<TeamToListDto>>();
        return result ?? [];
    }

    public async Task<TeamToListDto> Add(string name, string description, IEnumerable<Guid> userIds, IEnumerable<Guid> applicationIds)
    {
        var response = await _client.PostAsJsonAsync(NummyConstants.AddTeamUrl,
            new TeamToAddDto(name, description, userIds, applicationIds));

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TeamToListDto>();
        return result ?? throw new InvalidOperationException("Add team response was empty.");
    }

    public async Task<TeamToListDto?> Update(Guid id, string name, string description)
    {
        var response = await _client.PutAsJsonAsync(NummyConstants.UpdateTeamUrl + $"/{id}",
            new TeamToUpdateDto(name, description));

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<TeamToListDto>();
    }

    public async Task Delete(Guid id)
    {
        var response = await _client.DeleteAsync(NummyConstants.DeleteTeamUrl + $"/{id}");
        response.EnsureSuccessStatusCode();
    }
}
