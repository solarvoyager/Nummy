namespace NummyShared.DTOs;

public record TeamToListDto(
    Guid Id,
    string Name,
    string Description,
    DateTimeOffset CreatedAt
);

public record TeamToAddDto(
    string Name,
    string Description
);

public record TeamToUpdateDto(
    string Name,
    string Description
);