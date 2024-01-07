namespace NummyApi.Dtos;

public record RequestLogToAddDto(
    Guid HttpLogId,
    string TraceIdentifier,
    string Body,
    string Method,
    string Path,
    string? RemoteIp
);