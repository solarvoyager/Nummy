namespace NummyShared.DTOs.Domain;

public record TotalCountsResponseDto(
    int TotalRequests,
    int RequestsToday,
    int RequestsThisWeek,
    List<WeeklyRequestDto> WeeklyRequests,
    int RequestsThisHour,
    List<HourlyRequestDto> HourlyRequests,
    int TotalCodeLogs,
    int TodayCodeLogs,
    int TodayErrorAndFatals,
    int TotalErrorAndFatals
);

public record WeeklyRequestDto(
    string Day,
    int Count
);

public record HourlyRequestDto(
    string Hour,
    int Count
);

public record ServiceUrlResponseDto(
    string ServiceUrl
);

public record HttpLogDto(
    Guid Id,
    Guid HttpLogId,
    string TraceIdentifier,
    string? RequestBody,
    List<HeaderDto> RequestHeaders,
    string? ResponseBody,
    List<HeaderDto>? ResponseHeaders,
    int? StatusCode,
    long? DurationMs,
    DateTimeOffset? CreatedAt
);

public record HeaderDto
(
    string Key,
    string Value
);