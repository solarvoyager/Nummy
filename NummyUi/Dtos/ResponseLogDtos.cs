namespace NummyUi.Dtos;

public record ResponseLogToListDto(
    Guid Id,
    Guid HttpLogId,
    string Body,
    int StatusCode,
    DateTimeOffset CreatedAt
);