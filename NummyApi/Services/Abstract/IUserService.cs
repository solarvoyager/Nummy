using NummyShared.DTOs;

namespace NummyApi.Services.Abstract;

public interface IUserService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
    Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken = default);
    Task<UserToListDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserToListDto>> GetAsync(CancellationToken cancellationToken = default);
}
