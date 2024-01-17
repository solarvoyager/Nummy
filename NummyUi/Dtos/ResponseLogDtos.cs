namespace NummyUi.Dtos;

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