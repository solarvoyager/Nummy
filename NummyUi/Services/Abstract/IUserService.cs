using NummyShared.DTOs;

namespace NummyUi.Services.Abstract;

public interface IUserService
{
    Task<LoginResponseDto> Login(string email, string password);
    Task<RegisterResponseDto> Register(string name, string surname, string email, string? phone, string password);
    Task<UserToListDto> Get(Guid id);
    Task<IEnumerable<UserToListDto>> Get();
}
