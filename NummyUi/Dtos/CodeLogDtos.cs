﻿using NummyUi.Dtos.Enums;

namespace NummyUi.Dtos;

public record CodeLogToAddDto(
    string? TraceIdentifier,
    CodeLogLevel LogLevel,
    string Title,
    string? Description,
    string? StackTrace,
    string? InnerException,
    string? ExceptionType
);

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