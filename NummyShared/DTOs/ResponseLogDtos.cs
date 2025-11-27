namespace NummyShared.DTOs;

public record ResponseLogToAddDto(
    Guid HttpLogId,
    string? Body,
    int StatusCode,
    long DurationMs,
    string? Headers
);

public record ResponseLogToListDto(
    Guid Id,
    Guid HttpLogId,
    string? Body,
    int StatusCode,
    long DurationMs,
    string? Headers,
    DateTimeOffset CreatedAt
);