using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Dtos;
using NummyApi.Dtos.Domain;
using NummyApi.Dtos.Generic;
using NummyApi.Entitites;
using NummyApi.Services.Abstract;

namespace NummyApi.Services.Concrete;

public class RequestLogService(NummyDataContext dataContext, IMapper mapper) : IRequestLogService
{
    public async Task Add(RequestLogToAddDto dto)
    {
        var mapped = mapper.Map<RequestLog>(dto);

        await dataContext.AddAsync(mapped);
        await dataContext.SaveChangesAsync();
    }

    public async Task<PaginatedListDto<RequestLogToListDto>> Get(GetRequestLogsDto dto)
    {
        var skip = (dto.PageIndex - 1) * dto.PageSize;

        var query = dataContext.RequestLogs.Where(l => true);

        if (!string.IsNullOrWhiteSpace(dto.Query))
            query = query.Where(l =>
                EF.Functions.Like(l.TraceIdentifier.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(l.Body.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(l.Method.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(l.Path.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(l.RemoteIp!.ToLower(), $"%{dto.Query.ToLower()}%"));

        var totalCount = await query.CountAsync();

        if (dto.SortType is not null && dto.SortOrder is not null)
            query = dto.SortType switch
            {
                RequestLogSortType.TraceIdentifier => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.TraceIdentifier)
                    : query.OrderBy(q => q.TraceIdentifier),
                RequestLogSortType.Body => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.Body)
                    : query.OrderBy(q => q.Body),
                RequestLogSortType.Method => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.Method)
                    : query.OrderBy(q => q.Method),
                RequestLogSortType.Path => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.Path)
                    : query.OrderBy(q => q.Path),
                RequestLogSortType.RemoteIp => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.RemoteIp)
                    : query.OrderBy(q => q.RemoteIp),
                _ => query
            };

        query = query
            .Skip(skip)
            .Take(dto.PageSize);

        var mapped = mapper.Map<IEnumerable<RequestLogToListDto>>(await query.ToListAsync());

        return new PaginatedListDto<RequestLogToListDto>(totalCount, mapped);
    }

    public async Task<bool> Delete(DeleteRequestLogsDto dto)
    {
        await dataContext.RequestLogs.Where(l => dto.Ids.Contains(l.Id)).ExecuteDeleteAsync();

        return true;
    }
}