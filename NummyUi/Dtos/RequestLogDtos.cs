namespace NummyUi.Dtos;

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
    string Body,
    string Method,
    string Path,
    string? RemoteIp,
    DateTimeOffset CreatedAt
);