using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Dtos;
using NummyApi.Dtos.Domain;
using NummyApi.Dtos.Enums;
using NummyApi.Dtos.Generic;
using NummyApi.Entitites;
using NummyApi.Services.Abstract;

namespace NummyApi.Services.Concrete;

public class StatisticalService(NummyDataContext dataContext, IMapper mapper) : IStatisticalService
{
    public async Task<TotalCountsResponseDto> GetTotalCountsAsync()
    {
        return new TotalCountsResponseDto(
            TotalRequests: await dataContext.RequestLogs
                .CountAsync(),
            RequestsToday: await dataContext.RequestLogs
                .Where(l => l.CreatedAt.Year == DateTimeOffset.Now.Year
                            && l.CreatedAt.Month == DateTimeOffset.Now.Month
                            && l.CreatedAt.Day == DateTimeOffset.Now.Day)
                .CountAsync(),
            RequestsThisHour: await dataContext.RequestLogs
                .Where(l => l.CreatedAt.Year == DateTimeOffset.Now.Year
                            && l.CreatedAt.Month == DateTimeOffset.Now.Month
                            && l.CreatedAt.Day == DateTimeOffset.Now.Day
                            && l.CreatedAt.Hour == DateTimeOffset.Now.Hour)
                .CountAsync(),
            HourlyRequests: new List<int>(), // TODO,
            RequestsThisWeek: 0, // TODO,
            WeeklyRequests: new List<int>(), // Todo//await dataContext.RequestLogs.Where(l => l.CreatedAt.DayOfWeek > DateTimeOffset.Now.DayOfWeek )
            TotalCodeLogs: await dataContext.CodeLogs
                .CountAsync(),
            TodayErrorAndFatals: await dataContext.CodeLogs
                .Where(l => (l.LogLevel == CodeLogLevel.Error || l.LogLevel == CodeLogLevel.Fatal)
                            && l.CreatedAt.Year == DateTimeOffset.Now.Year
                            && l.CreatedAt.Month == DateTimeOffset.Now.Month
                            && l.CreatedAt.Day == DateTimeOffset.Now.Day)
                .CountAsync(),
            TotalErrorAndFatals: await dataContext.CodeLogs
                .Where(l => l.LogLevel == CodeLogLevel.Error || l.LogLevel == CodeLogLevel.Fatal)
                .CountAsync()
        );
    }
}