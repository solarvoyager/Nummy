namespace NummyShared.DTOs;

public record TeamToListDto(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedAt
);

public record TeamToAddDto(
    string Name,
    string Description
);

public record TeamToUpdateDto(
    string Name,
    string Description
);