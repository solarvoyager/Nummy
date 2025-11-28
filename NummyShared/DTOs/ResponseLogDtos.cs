namespace NummyShared.DTOs;

public record ResponseLogToAddDto(
    Guid HttpLogId,
    string? Body,
    int StatusCode,
    long DurationMs,
    List<HeaderToAddDto> Headers
);

public record ResponseLogToListDto(
    Guid Id,
    Guid HttpLogId,
    string? Body,
    int StatusCode,
    long DurationMs,
    List<HeaderToListDto> Headers,
    DateTimeOffset CreatedAt
);