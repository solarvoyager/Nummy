using NummyShared.DTOs;
using NummyUi.Services.Abstract;
using NummyUi.Utils;

namespace NummyUi.Services;

public class UserService(IHttpClientFactory clientFactory) : IUserService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyConstants.ClientName);

    public async Task<LoginResponseDto> Login(string email, string password)
    {
        var response = await _client.PostAsJsonAsync(NummyConstants.LoginUrl,
            new LoginRequestDto(email, password));

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        return result ?? throw new InvalidOperationException("Login response was empty.");
    }

    public async Task<RegisterResponseDto> Register(string name, string surname, string email, string? phone, string password)
    {
        var response = await _client.PostAsJsonAsync(NummyConstants.RegisterUrl,
            new RegisterRequestDto(name, surname, email, phone, password));

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<RegisterResponseDto>();
        return result ?? throw new InvalidOperationException("Register response was empty.");
    }

    public async Task<UserToListDto> Get(Guid id)
    {
        var response = await _client.GetAsync(NummyConstants.GetUserUrl + $"/{id}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<UserToListDto>();
        return result ?? throw new InvalidOperationException($"User '{id}' response was empty.");
    }

    public async Task<IEnumerable<UserToListDto>> Get()
    {
        var response = await _client.GetAsync(NummyConstants.GetUserUrl);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<UserToListDto>>();
        return result ?? [];
    }
}
