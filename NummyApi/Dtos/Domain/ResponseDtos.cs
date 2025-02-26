namespace NummyApi.Dtos.Domain;

public record TotalCountsResponseDto
(
    int TotalRequests,
    int RequestsToday,
    int RequestsThisHour,
    List<int> HourlyRequests,
    int RequestsThisWeek,
    List<WeeklyRequestDto> WeeklyRequests,

    int TotalCodeLogs,
    int TodayErrorAndFatals,
    int TotalErrorAndFatals
);

public record WeeklyRequestDto
(
    string Day,
    int Count
);