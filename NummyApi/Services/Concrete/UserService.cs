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
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || !SecurityHelper.ValidatePassword(request.Password, user.PasswordHash, user.PasswordSalt))
            return new LoginResponseDto(false, "Invalid email or password", null);

        user.LastLoginDate = DateTimeOffset.Now;
        await context.SaveChangesAsync();

        return new LoginResponseDto(true, "Login successful", user.Id);
    }

    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        // Check if email already exists
        if (await context.Users.AnyAsync(u => u.Email == request.Email))
            return new RegisterResponseDto(false, "Email already exists");

        // Generate password hash and salt
        var (hash, salt) = SecurityHelper.GeneratePasswordHash(request.Password);

        var user = new User
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            PasswordHash = hash,
            PasswordSalt = salt,
            LastLoginDate = DateTimeOffset.Now
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return new RegisterResponseDto(true, "Registration successful");
    }

    public async Task<UserToListDto?> GetAsync(Guid id)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return null;

        var mapped = mapper.Map<UserToListDto>(user);

        return mapped;
    }

    public async Task<IEnumerable<UserToListDto>> GetAsync()
    {
        var users = await context.Users.ToListAsync();

        var mappeds = mapper.Map<List<UserToListDto>>(users);

        return mappeds;
    }
}