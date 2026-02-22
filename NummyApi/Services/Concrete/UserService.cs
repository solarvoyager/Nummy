using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Entitites;
using NummyApi.Helpers;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;

namespace NummyApi.Services.Concrete;

public class UserService(NummyDataContext context, IMapper mapper) : IUserService
{
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null || !SecurityHelper.ValidatePassword(request.Password, user.PasswordHash, user.PasswordSalt))
            return new LoginResponseDto(false, "Invalid email or password", null);

        user.LastLoginDate = DateTimeOffset.Now;
        await context.SaveChangesAsync(cancellationToken);

        return new LoginResponseDto(true, "Login successful", user.Id);
    }

    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken = default)
    {
        if (await context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            return new RegisterResponseDto(false, "Email already exists");

        var (hash, salt) = SecurityHelper.GeneratePasswordHash(request.Password);
        var avatarColorHex = UtilHelper.GenerateRandomColorHex();

        var user = new User
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            AvatarColorHex = avatarColorHex,
            PasswordHash = hash,
            PasswordSalt = salt,
            LastLoginDate = DateTimeOffset.Now
        };

        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new RegisterResponseDto(true, "Registration successful");
    }

    public async Task<UserToListDto?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (user == null)
            return null;

        return mapper.Map<UserToListDto>(user);
    }

    public async Task<IEnumerable<UserToListDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        var users = await context.Users.ToListAsync(cancellationToken);
        return mapper.Map<List<UserToListDto>>(users);
    }
}
