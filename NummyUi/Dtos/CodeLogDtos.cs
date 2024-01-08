using NummyApi.Enums;

namespace NummyUi.Dtos;

public record CodeLogToListDto(
    Guid Id,
    string? TraceIdentifier,
    CodeLogLevel LogLevel,
    string Title,
    string? Description,
    string? StackTrace,
    string? InnerException,
    string? ExceptionType,
    DateTimeOffset CreatedAt
);