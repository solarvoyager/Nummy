using NummyShared.DTOs;

namespace NummyApi.Services.Abstract;

public interface IUserService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
    Task<UserToListDto?> GetAsync(Guid id);
    Task<IEnumerable<UserToListDto>> GetAsync();
}