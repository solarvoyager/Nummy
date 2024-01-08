namespace NummyUi.Dtos;

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