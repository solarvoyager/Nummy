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

public class LogService(NummyDataContext dataContext, IMapper mapper) : ILogService
{
    public async Task AddRequestLog(RequestLogToAddDto dto)
    {
        var mapped = mapper.Map<RequestLog>(dto);

        await dataContext.AddAsync(mapped);
        await dataContext.SaveChangesAsync();
    }

    public async Task AddResponseLog(ResponseLogToAddDto dto)
    {
        var mapped = mapper.Map<ResponseLog>(dto);

        await dataContext.AddAsync(mapped);
        await dataContext.SaveChangesAsync();
    }

    public async Task AddCodeLog(CodeLogToAddDto dto)
    {
        var mapped = mapper.Map<CodeLog>(dto);

        await dataContext.AddAsync(mapped);
        await dataContext.SaveChangesAsync();
    }

    public async Task<PaginatedListDto<RequestLogToListDto>> GetRequestLogs(int pageIndex, int pageSize)
    {
        var skip = (pageIndex - 1) * pageSize;

        var data = await dataContext.RequestLogs
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await dataContext.RequestLogs.CountAsync();

        var mapped = mapper.Map<IEnumerable<RequestLogToListDto>>(data);

        return new PaginatedListDto<RequestLogToListDto>(totalCount, mapped);
    }

    public async Task<PaginatedListDto<ResponseLogToListDto>> GetResponseLogs(int pageIndex, int pageSize)
    {
        var skip = (pageIndex - 1) * pageSize;

        var data = await dataContext.ResponseLogs
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await dataContext.ResponseLogs.CountAsync();

        var mapped = mapper.Map<IEnumerable<ResponseLogToListDto>>(data);

        return new PaginatedListDto<ResponseLogToListDto>(totalCount, mapped);
    }

    public async Task<PaginatedListDto<CodeLogToListDto>> GetCodeLogs(GetCodeLogsRequestDto dto)
    {
        var skip = (dto.PageIndex - 1) * dto.PageSize;

        var query = dataContext.CodeLogs
            .Where(l => dto.Levels.Contains(l.LogLevel));

        if (!string.IsNullOrWhiteSpace(dto.Query))
        {
            query = query.Where(l =>
                EF.Functions.Like(l.TraceIdentifier!.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(l.Title.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(l.Description!.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(l.ExceptionType!.ToLower(), $"%{dto.Query.ToLower()}%"));
        }

        var totalCount = await query.CountAsync();

        query = query
            .Skip(skip)
            .Take(dto.PageSize);

        if (dto.SortType is not null && dto.SortOrder is not null)
        {
            query = dto.SortType switch
            {
                CodeLogSortType.TraceIdentifier => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.TraceIdentifier)
                    : query.OrderBy(q => q.TraceIdentifier),
                CodeLogSortType.LogLevel => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.LogLevel)
                    : query.OrderBy(q => q.LogLevel),
                CodeLogSortType.Title => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.Title)
                    : query.OrderBy(q => q.Title),
                CodeLogSortType.Description => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.Description)
                    : query.OrderBy(q => q.Description),
                CodeLogSortType.ExceptionType => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.ExceptionType)
                    : query.OrderBy(q => q.ExceptionType),
                CodeLogSortType.CreatedAt => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.CreatedAt)
                    : query.OrderBy(q => q.CreatedAt),
                _ => query
            };
        }

        var mapped = mapper.Map<IEnumerable<CodeLogToListDto>>(await query.ToListAsync());

        return new PaginatedListDto<CodeLogToListDto>(totalCount, mapped);
    }
}