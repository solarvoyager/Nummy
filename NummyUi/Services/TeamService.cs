using NummyShared.DTOs;
using NummyUi.Utils;

namespace NummyUi.Services
{
    public interface ITeamService
    {
        Task<TeamToListDto?> Get(Guid id);
        Task<IEnumerable<TeamToListDto>> Get();
        Task<TeamToListDto> Add(string name, string description, IEnumerable<Guid> userIds, IEnumerable<Guid> applicationIds);
        Task<TeamToListDto?> Update(Guid id, string name, string description);
        Task Delete(Guid id);
    }

    public class TeamService(IHttpClientFactory clientFactory) : ITeamService
    {
        private readonly HttpClient _client = clientFactory.CreateClient(NummyConstants.ClientName);

        public async Task<TeamToListDto?> Get(Guid id)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(NummyConstants.GetTeamsUrl + $"/{id}", UriKind.Relative)
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
                RequestUri = new Uri(NummyConstants.GetTeamsUrl, UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<TeamToListDto>>();

            return result!;
        }

        public async Task<TeamToListDto> Add(string name, string description, IEnumerable<Guid> userIds, IEnumerable<Guid> applicationIds)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(new TeamToAddDto(name, description, userIds, applicationIds)),
                Method = HttpMethod.Post,
                RequestUri = new Uri(NummyConstants.AddTeamUrl, UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<TeamToListDto>();

            return result!;
        }

        public async Task<TeamToListDto?> Update(Guid id, string name, string description)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(new TeamToUpdateDto(name, description)),
                Method = HttpMethod.Put,
                RequestUri = new Uri(NummyConstants.UpdateTeamUrl + $"/{id}", UriKind.Relative)
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
                RequestUri = new Uri(NummyConstants.DeleteTeamUrl + $"/{id}", UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}