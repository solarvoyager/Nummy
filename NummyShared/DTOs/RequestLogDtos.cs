namespace NummyShared.Dtos;

public record RequestLogToAddDto(
    Guid HttpLogId,
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
    // moved to response log to list dto
    //string Body,
    int ResponseStatusCode,
    string Method,
    string Path,
    string? RemoteIp,
    DateTimeOffset CreatedAt
);