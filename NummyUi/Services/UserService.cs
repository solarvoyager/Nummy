using NummyShared.DTOs;
using NummyUi.Utils;

namespace NummyUi.Services
{
    public interface IUserService
    {
        Task<LoginResponseDto> Login(string email, string password);

        Task<RegisterResponseDto> Register(
            string name,
            string surname,
            string email,
            string? phone,
            string password);

        Task<UserToListDto> Get(Guid id);
        Task<IEnumerable<UserToListDto>> Get();
    }

    public class UserService(IHttpClientFactory clientFactory) : IUserService
    {
        private readonly HttpClient _client = clientFactory.CreateClient(NummyContants.ClientName);

        public async Task<LoginResponseDto> Login(string email, string password)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(new LoginRequestDto(email, password)),
                Method = HttpMethod.Post,
                RequestUri = new Uri(NummyContants.LoginUrl, UriKind.Relative)
            };

            var response = await _client.SendAsync(request);

            var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

            return result!;
        }

        public async Task<RegisterResponseDto> Register(string name,
            string surname,
            string email,
            string? phone,
            string password)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(
                    new RegisterRequestDto(
                        name,
                        surname,
                        email,
                        phone,
                        password
                    )),
                Method = HttpMethod.Post,
                RequestUri = new Uri(NummyContants.RegisterUrl, UriKind.Relative)
            };

            var response = await _client.SendAsync(request);

            var result = await response.Content.ReadFromJsonAsync<RegisterResponseDto>();

            return result!;
        }

        public async Task<UserToListDto> Get(Guid id)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(NummyContants.GetUserUrl + $"/{id}", UriKind.Relative)
            };

            var response = await _client.SendAsync(request);

            var result = await response.Content.ReadFromJsonAsync<UserToListDto>();

            return result!;
        }
        
        public async Task<IEnumerable<UserToListDto>> Get()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(NummyContants.GetUserUrl, UriKind.Relative)
            };

            var response = await _client.SendAsync(request);

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<UserToListDto>>();

            return result!;
        }
    }
}