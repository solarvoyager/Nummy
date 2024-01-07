using NummyApi.Enums;

namespace NummyApi.Dtos;

public record CodeLogToAddDto(
    string? TraceIdentifier,
    CodeLogLevel LogLevel,
    string Title,
    string? Description,
    string? StackTrace,
    string? InnerException,
    string? ExceptionType
);