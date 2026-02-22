using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Services.Abstract;
using NummyShared.DTOs.Domain;
using NummyShared.DTOs.Enums;

namespace NummyApi.Services.Concrete;

public class StatisticalService(NummyDataContext dataContext) : IStatisticalService
{
    public async Task<TotalCountsResponseDto> GetTotalCountsAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.Now;

        // Run independent queries sequentially (EF Core DbContext is not thread-safe).
        // Fix: use date range comparisons instead of component extractions for index-friendly queries.
        var totalRequests = await dataContext.RequestLogs.CountAsync(cancellationToken);
        var requestsToday = await CountRequestsByDateAsync(now, cancellationToken);
        var requestsThisHour = await CountRequestsByHourAsync(now, cancellationToken);
        var hourlyRequests = await CountRequestsGroupedByHourAsync(now, cancellationToken);
        var requestsThisWeek = await CountRequestsByWeekAsync(now, cancellationToken);
        var weeklyRequests = await CountRequestsGroupedByDayOfWeekAsync(now, cancellationToken);
        var totalCodeLogs = await dataContext.CodeLogs.CountAsync(cancellationToken);
        var todayCodeLogs = await CountCodeLogsByDateAsync(now, cancellationToken);
        var todayErrorAndFatals = await CountErrorAndFatalLogsByDateAsync(now, cancellationToken);
        var totalErrorAndFatals = await CountErrorAndFatalLogsAsync(cancellationToken);

        return new TotalCountsResponseDto(
            TotalRequests: totalRequests,
            RequestsToday: requestsToday,
            RequestsThisHour: requestsThisHour,
            HourlyRequests: hourlyRequests,
            RequestsThisWeek: requestsThisWeek,
            WeeklyRequests: weeklyRequests,
            TotalCodeLogs: totalCodeLogs,
            TodayCodeLogs: todayCodeLogs,
            TodayErrorAndFatals: todayErrorAndFatals,
            TotalErrorAndFatals: totalErrorAndFatals
        );
    }

    private async Task<int> CountRequestsByDateAsync(DateTimeOffset date, CancellationToken cancellationToken)
    {
        var start = date.Date;
        var end = start.AddDays(1);
        return await dataContext.RequestLogs
            .Where(l => l.CreatedAt >= start && l.CreatedAt < end)
            .CountAsync(cancellationToken);
    }

    private async Task<int> CountRequestsByHourAsync(DateTimeOffset date, CancellationToken cancellationToken)
    {
        var start = new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, 0, 0, date.Offset);
        var end = start.AddHours(1);
        return await dataContext.RequestLogs
            .Where(l => l.CreatedAt >= start && l.CreatedAt < end)
            .CountAsync(cancellationToken);
    }

    private async Task<List<HourlyRequestDto>> CountRequestsGroupedByHourAsync(DateTimeOffset date, CancellationToken cancellationToken)
    {
        var start = date.Date;
        var end = start.AddDays(1);

        var result = await dataContext.RequestLogs
            .Where(l => l.CreatedAt >= start && l.CreatedAt < end)
            .GroupBy(l => l.CreatedAt.Hour)
            .OrderBy(g => g.Key)
            .Select(g => new HourlyRequestDto(
                $"{g.Key:00}:00",
                g.Count()
            ))
            .ToListAsync(cancellationToken);

        for (var hour = 0; hour < 24; hour++)
            if (result.All(r => r.Hour != $"{hour:00}:00"))
                result.Add(new HourlyRequestDto($"{hour:00}:00", 0));

        return result.OrderBy(r => r.Hour).ToList();
    }

    private async Task<int> CountRequestsByWeekAsync(DateTimeOffset date, CancellationToken cancellationToken)
    {
        var start = date.AddDays(-7);
        var end = date.Date.AddDays(1);
        return await dataContext.RequestLogs
            .Where(l => l.CreatedAt >= start && l.CreatedAt < end)
            .CountAsync(cancellationToken);
    }

    private async Task<List<WeeklyRequestDto>> CountRequestsGroupedByDayOfWeekAsync(DateTimeOffset date, CancellationToken cancellationToken)
    {
        var start = date.AddDays(-7);
        var end = date.Date.AddDays(1);

        var result = await dataContext.RequestLogs
            .Where(l => l.CreatedAt >= start && l.CreatedAt < end)
            .GroupBy(l => l.CreatedAt.DayOfWeek)
            .OrderBy(g => (int)g.Key)
            .Select(g => new WeeklyRequestDto(
                g.Key.ToString(),
                g.Count()
            ))
            .ToListAsync(cancellationToken);

        var allDays = Enum.GetValues<DayOfWeek>().Cast<DayOfWeek>();
        foreach (var day in allDays)
            if (result.All(r => r.Day != day.ToString()))
                result.Add(new WeeklyRequestDto(day.ToString(), 0));

        return result.OrderBy(r => (int)Enum.Parse<DayOfWeek>(r.Day)).ToList();
    }

    private async Task<int> CountErrorAndFatalLogsByDateAsync(DateTimeOffset date, CancellationToken cancellationToken)
    {
        var start = date.Date;
        var end = start.AddDays(1);
        return await dataContext.CodeLogs
            .Where(l => (l.LogLevel == CodeLogLevel.Error || l.LogLevel == CodeLogLevel.Fatal)
                        && l.CreatedAt >= start && l.CreatedAt < end)
            .CountAsync(cancellationToken);
    }

    private async Task<int> CountErrorAndFatalLogsAsync(CancellationToken cancellationToken)
    {
        return await dataContext.CodeLogs
            .Where(l => l.LogLevel == CodeLogLevel.Error || l.LogLevel == CodeLogLevel.Fatal)
            .CountAsync(cancellationToken);
    }

    private async Task<int> CountCodeLogsByDateAsync(DateTimeOffset date, CancellationToken cancellationToken)
    {
        var start = date.Date;
        var end = start.AddDays(1);
        return await dataContext.CodeLogs
            .Where(l => l.CreatedAt >= start && l.CreatedAt < end)
            .CountAsync(cancellationToken);
    }
}
