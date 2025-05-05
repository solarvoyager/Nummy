namespace NummyShared.DTOs;

public record TeamToListDto(
    Guid Id,
    string Name,
    string Description,
    string AvatarColorHex,
    DateTimeOffset CreatedAt,
    List<UserToListDto> Users,
    List<ApplicationToListDto> Applications
);

public record TeamToAddDto(
    string Name,
    string Description,
    IEnumerable<Guid> UserIds,
    IEnumerable<Guid> ApplicationIds
);

public record TeamToUpdateDto(
    string Name,
    string Description
);