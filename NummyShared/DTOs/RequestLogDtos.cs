namespace NummyShared.DTOs;

public record RequestLogToAddDto(
    Guid HttpLogId,
    Guid ApplicationId,
    string TraceIdentifier,
    string Body,
    string Method,
    string Path,
    string? RemoteIp
);

public record RequestLogToListDto(
    Guid Id,
    Guid HttpLogId,
    string TraceIdentifier,
    Guid ApplicationId,
    // moved to response log to list dto
    //string Body,
    int ResponseStatusCode,
    string Method,
    string Path,
    string? RemoteIp,
    DateTimeOffset CreatedAt
);