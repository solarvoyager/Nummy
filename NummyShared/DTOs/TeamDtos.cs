namespace NummyShared.DTOs;

public record TeamToListDto(
    Guid Id,
    string Name,
    string Description,
    DateTimeOffset CreatedAt,
    List<UserToListDto> Users,
    List<ApplicationToListDto> Applications
);

public record TeamToAddDto(
    string Name,
    string Description
);

public record TeamToUpdateDto(
    string Name,
    string Description
);