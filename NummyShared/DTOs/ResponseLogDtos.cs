namespace NummyShared.DTOs;

public record ResponseLogToAddDto(
    Guid HttpLogId,
    string Body,
    int StatusCode
);

public record ResponseLogToListDto(
    Guid Id,
    Guid HttpLogId,
    string Body,
    int StatusCode,
    DateTimeOffset CreatedAt
);

public record ResponseLogDto(
    Guid Id,
    Guid HttpLogId,
    string RequestBody,
    string ResponseBody,
    int StatusCode,
    DateTimeOffset CreatedAt,
    TimeSpan Duration
);