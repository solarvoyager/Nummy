using NummyShared.DTOs.Enums;

namespace NummyShared.DTOs;

public record CodeLogToAddDto(
    string? TraceIdentifier,
    Guid ApplicationId,
    CodeLogLevel LogLevel,
    string Title,
    string? Description,
    string? StackTrace,
    string? InnerException,
    string? ExceptionType
);

public record CodeLogToListDto(
    Guid Id,
    Guid ApplicationId,
    string? TraceIdentifier,
    CodeLogLevel LogLevel,
    string Title,
    string? Description,
    string? StackTrace,
    string? InnerException,
    string? ExceptionType,
    DateTimeOffset CreatedAt
);