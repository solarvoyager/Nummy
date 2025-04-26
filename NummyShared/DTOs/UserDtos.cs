namespace NummyShared.DTOs;

public record LoginRequestDto(
    string Email,
    string Password
);

public record LoginResponseDto(
    bool Success,
    string? Message,
    Guid? UserId
);

public record RegisterRequestDto(
    string Name,
    string Surname,
    string Email,
    string? Phone,
    string Password
);

public record RegisterResponseDto(
    bool Success,
    string? Message
);

public record UserToListDto(
    Guid Id,
    string Name,
    string Surname,
    string Email,
    string? Phone,
    string AvatarColorHex
);