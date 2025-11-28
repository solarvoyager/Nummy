namespace NummyShared.DTOs;

public record RequestLogToAddDto(
    Guid HttpLogId,
    Guid ApplicationId,
    string TraceIdentifier,
    string? Body,
    string Method,
    string Path,
    string? RemoteIp,
    List<HeaderToListDto> Headers
);

public record RequestLogToListDto(
    Guid Id,
    Guid HttpLogId,
    string TraceIdentifier,
    Guid ApplicationId,
    int ResponseStatusCode,
    long ResponseDurationMs,
    string Method,
    string Path,
    string? RemoteIp,
    DateTimeOffset CreatedAt
);