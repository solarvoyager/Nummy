using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Services.Abstract;
using NummyShared.Dtos.Domain;
using NummyShared.Dtos.Enums;

namespace NummyApi.Services.Concrete;

public class StatisticalService(NummyDataContext dataContext) : IStatisticalService
{
    public async Task<TotalCountsResponseDto> GetTotalCountsAsync()
    {
        var now = DateTimeOffset.Now;
        return new TotalCountsResponseDto(
            TotalRequests: await dataContext.RequestLogs.CountAsync(),
            RequestsToday: await CountRequestsByDateAsync(now),
            RequestsThisHour: await CountRequestsByHourAsync(now),
            HourlyRequests: await CountRequestsGroupedByHourAsync(now),
            RequestsThisWeek: await CountRequestsByWeekAsync(now),
            WeeklyRequests: await CountRequestsGroupedByDayOfWeekAsync(now),
            TotalCodeLogs: await dataContext.CodeLogs.CountAsync(),
            TodayCodeLogs: await CountCodeLogsByDate(now),
            TodayErrorAndFatals: await CountErrorAndFatalLogsByDateAsync(now),
            TotalErrorAndFatals: await CountErrorAndFatalLogsAsync()
        );
    }

    private async Task<int> CountRequestsByDateAsync(DateTimeOffset date)
    {
        return await dataContext.RequestLogs
            .Where(l => l.CreatedAt.Year == date.Year
                        && l.CreatedAt.Month == date.Month
                        && l.CreatedAt.Day == date.Day)
            .CountAsync();
    }

    private async Task<int> CountRequestsByHourAsync(DateTimeOffset date)
    {
        return await dataContext.RequestLogs
            .Where(l => l.CreatedAt.Year == date.Year
                        && l.CreatedAt.Month == date.Month
                        && l.CreatedAt.Day == date.Day
                        && l.CreatedAt.Hour == date.Hour)
            .CountAsync();
    }

    private async Task<List<HourlyRequestDto>> CountRequestsGroupedByHourAsync(DateTimeOffset date)
    {
        var result = await dataContext.RequestLogs
            .Where(l => l.CreatedAt >= date.Date && l.CreatedAt < date.Date.AddDays(1))
            .GroupBy(l => l.CreatedAt.Hour)
            .OrderBy(g => g.Key)
            .Select(g => new HourlyRequestDto(
                $"{g.Key:00}:00",
                g.Count()
            ))
            .ToListAsync();

        for (int hour = 0; hour < 24; hour++)
        {
            if (result.All(r => r.Hour != $"{hour:00}:00"))
            {
                result.Add(new HourlyRequestDto($"{hour:00}:00", 0));
            }
        }

        return result.OrderBy(r => r.Hour).ToList();
    }

    private async Task<int> CountRequestsByWeekAsync(DateTimeOffset date)
    {
        return await dataContext.RequestLogs
            .Where(l => l.CreatedAt >= date.AddDays(-7) && l.CreatedAt < date.AddDays(1))
            .CountAsync();
    }

    private async Task<List<WeeklyRequestDto>> CountRequestsGroupedByDayOfWeekAsync(DateTimeOffset date)
    {
        var result = await dataContext.RequestLogs
            .Where(l => l.CreatedAt >= date.AddDays(-7) && l.CreatedAt < date.AddDays(1))
            .GroupBy(l => l.CreatedAt.DayOfWeek)
            .OrderBy(g => (int)g.Key)
            .Select(g => new WeeklyRequestDto(
                g.Key.ToString(),
                g.Count()
            ))
            .ToListAsync();

        var allDays = Enum.GetValues<DayOfWeek>().Cast<DayOfWeek>();
        foreach (var day in allDays)
        {
            if (result.All(r => r.Day != day.ToString()))
            {
                result.Add(new WeeklyRequestDto(day.ToString(), 0));
            }
        }

        return result.OrderBy(r => (int)Enum.Parse<DayOfWeek>(r.Day)).ToList();
    }

    private async Task<int> CountErrorAndFatalLogsByDateAsync(DateTimeOffset date)
    {
        return await dataContext.CodeLogs
            .Where(l => (l.LogLevel == CodeLogLevel.Error || l.LogLevel == CodeLogLevel.Fatal)
                        && l.CreatedAt.Year == date.Year
                        && l.CreatedAt.Month == date.Month
                        && l.CreatedAt.Day == date.Day)
            .CountAsync();
    }

    private async Task<int> CountErrorAndFatalLogsAsync()
    {
        return await dataContext.CodeLogs
            .Where(l => l.LogLevel == CodeLogLevel.Error || l.LogLevel == CodeLogLevel.Fatal)
            .CountAsync();
    }
    
    private async Task<int> CountCodeLogsByDate(DateTimeOffset date)
    {
        return await dataContext.CodeLogs
            .Where(l => l.CreatedAt.Year == date.Year
                        && l.CreatedAt.Month == date.Month
                        && l.CreatedAt.Day == date.Day)
            .CountAsync();
    }
}