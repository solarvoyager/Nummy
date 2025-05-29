using NummyShared.DTOs;
using NummyShared.DTOs.Enums;
using NummyUi.Utils;

namespace NummyUi.Services
{
    public interface IApplicationService
    {
        Task<ApplicationToListDto?> Get(Guid id);
        Task<IEnumerable<ApplicationToListDto>> Get();
        Task<IEnumerable<ApplicationStackToListDto>> GetStackType();
        Task<ApplicationToListDto> Add(string name, string description, Guid stackTypeId);
        Task<ApplicationToListDto?> Update(Guid id, string name, string description, Guid stackTypeId);
        Task Delete(Guid id);
    }

    public class ApplicationService(IHttpClientFactory clientFactory) : IApplicationService
    {
        private readonly HttpClient _client = clientFactory.CreateClient(NummyContants.ClientName);

        public async Task<ApplicationToListDto?> Get(Guid id)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(NummyContants.GetApplicationsUrl + $"/{id}", UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApplicationToListDto>();

            return result;
        }

        public async Task<IEnumerable<ApplicationToListDto>> Get()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(NummyContants.GetApplicationsUrl, UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<ApplicationToListDto>>();

            return result!;
        }

        public async Task<IEnumerable<ApplicationStackToListDto>> GetStackType()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(NummyContants.GetApplicationStackTypesUrl, UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<ApplicationStackToListDto>>();

            return result!;
        }

        public async Task<ApplicationToListDto> Add(string name, string description, Guid stackTypeId)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(new ApplicationToAddDto(name, description, stackTypeId)),
                Method = HttpMethod.Post,
                RequestUri = new Uri(NummyContants.AddApplicationUrl, UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApplicationToListDto>();

            return result!;
        }

        public async Task<ApplicationToListDto?> Update(Guid id, string name, string description, Guid stackTypeId)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(new ApplicationToUpdateDto(name, description, stackTypeId)),
                Method = HttpMethod.Put,
                RequestUri = new Uri(NummyContants.UpdateApplicationUrl + $"/{id}", UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApplicationToListDto>();

            return result;
        }

        public async Task Delete(Guid id)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(NummyContants.DeleteApplicationUrl + $"/{id}", UriKind.Relative)
            };

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
