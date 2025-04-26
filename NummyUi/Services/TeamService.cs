using NummyShared.DTOs;
using NummyUi.Utils;

namespace NummyUi.Services
{
    public interface ITeamService
    {
        Task<TeamToListDto?> Get(Guid id);
        Task<IEnumerable<TeamToListDto>> Get();
        Task<TeamToListDto> Add(TeamToAddDto dto);
        Task<TeamToListDto?> Update(Guid id, TeamToUpdateDto dto);
        Task Delete(Guid id);
        Task AddUserToTeam(Guid teamId, Guid userId);
    }

    public class TeamService(IHttpClientFactory clientFactory) : ITeamService
    {
        private readonly HttpClient _client = clientFactory.CreateClient(NummyContants.ClientName);

        public async Task<TeamToListDto?> Get(Guid id)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(NummyContants.GetTeamsUrl + $"/{id}", UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TeamToListDto>();

            return result;
        }

        public async Task<IEnumerable<TeamToListDto>> Get()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(NummyContants.GetTeamsUrl, UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<TeamToListDto>>();

            return result!;
        }

        public async Task<TeamToListDto> Add(TeamToAddDto dto)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(dto),
                Method = HttpMethod.Post,
                RequestUri = new Uri(NummyContants.AddTeamUrl, UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TeamToListDto>();

            return result!;
        }

        public async Task<TeamToListDto?> Update(Guid id, TeamToUpdateDto dto)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(dto),
                Method = HttpMethod.Put,
                RequestUri = new Uri(NummyContants.UpdateTeamUrl + $"/{id}", UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TeamToListDto>();

            return result;
        }

        public async Task Delete(Guid id)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(NummyContants.DeleteTeamUrl + $"/{id}", UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddUserToTeam(Guid teamId, Guid userId)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(NummyContants.AddUserToTeamUrl + $"/{teamId}/users/{userId}", UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}