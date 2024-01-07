namespace NummyApi.Dtos;

public record ResponseLogToAddDto(
    Guid HttpLogId,
    string Body,
    int StatusCode
);