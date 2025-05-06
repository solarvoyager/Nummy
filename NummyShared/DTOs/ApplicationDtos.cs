namespace NummyShared.DTOs;

public record ApplicationToAddDto(
    string Name,
    string Description
);

public record ApplicationToUpdateDto(
    string Name,
    string Description
);

public record ApplicationToListDto(
    Guid Id,
    string Name,
    string Description,
    DateTimeOffset CreatedAt,
    string AvatarColorHex
);