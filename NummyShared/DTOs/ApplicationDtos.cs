using NummyShared.DTOs.Enums;

namespace NummyShared.DTOs;

public record ApplicationToAddDto(
    string Name,
    string Description,
    string? HealthCheckerUrl,
    Guid StackId
);

public record ApplicationToUpdateDto(
    string Name,
    string Description,
    string? HealthCheckerUrl,
    Guid StackId
);

public record ApplicationToListDto(
    Guid Id,
    string Name,
    string Description,
    string? HealthCheckerUrl,
    bool? IsHealthy,
    DateTimeOffset CreatedAt,
    ApplicationStackToListDto Stack
);