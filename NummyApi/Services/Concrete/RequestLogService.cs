using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Entitites;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;
using NummyShared.DTOs.Domain;
using NummyShared.DTOs.Generic;

namespace NummyApi.Services.Concrete;

public class RequestLogService(NummyDataContext dataContext, IMapper mapper) : IRequestLogService
{
    public async Task<bool> Delete(DeleteRequestLogsDto dto)
    {
        await dataContext.RequestLogs
            .Where(l => dto.Ids.Contains(l.Id))
            .ExecuteDeleteAsync();

        return true;
    }

    public async Task Add(RequestLogToAddDto dto)
    {
        var mapped = mapper.Map<RequestLog>(dto);

        var added = await dataContext.AddAsync(mapped);
        await dataContext.SaveChangesAsync();
        
        var headers = dto.Headers.Select(h => new Header
        {
            Key = h.Key,
            Value = h.Value,
            RequestLogId = added.Entity.Id
        }).ToList();
        
        await dataContext.AddRangeAsync(headers);
        await dataContext.SaveChangesAsync();
    }

    public async Task<PaginatedListDto<RequestLogToListDto>> Get(Guid? applicationId, GetRequestLogsDto dto)
    {
        var skip = (dto.PageIndex - 1) * dto.PageSize;

        var query = dataContext.RequestLogs
            .Join(
                dataContext.ResponseLogs,
                request => request.HttpLogId,
                response => response.HttpLogId,
                (request, response) => new { Request = request, Response = response }
            )
            .Where(l=> applicationId == null || l.Request.ApplicationId == applicationId);

        if (!string.IsNullOrWhiteSpace(dto.Query))
            query = query.Where(x =>
                EF.Functions.Like(x.Request.TraceIdentifier.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(x.Request.Method.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(x.Request.Path.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(x.Request.RemoteIp!.ToLower(), $"%{dto.Query.ToLower()}"));

        var totalCount = await query.CountAsync();

        if (dto.SortType is not null && dto.SortOrder is not null)
            query = dto.SortType switch
            {
                RequestLogSortType.TraceIdentifier => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.Request.TraceIdentifier)
                    : query.OrderBy(q => q.Request.TraceIdentifier),
                RequestLogSortType.Method => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.Request.Method)
                    : query.OrderBy(q => q.Request.Method),
                RequestLogSortType.Path => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.Request.Path)
                    : query.OrderBy(q => q.Request.Path),
                RequestLogSortType.RemoteIp => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.Request.RemoteIp)
                    : query.OrderBy(q => q.Request.RemoteIp),
                RequestLogSortType.CreatedAt => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.Request.CreatedAt)
                    : query.OrderBy(q => q.Request.CreatedAt),
                _ => query
            };

        var result = await query
            .Skip(skip)
            .Take(dto.PageSize)
            .Select(x => new RequestLogToListDto(
                x.Request.Id,
                x.Request.HttpLogId,
                x.Request.TraceIdentifier,
                x.Request.ApplicationId,
                x.Response.StatusCode,
                x.Response.DurationMs,
                x.Request.Method,
                x.Request.Path,
                x.Request.RemoteIp,
                x.Request.CreatedAt
            ))
            .ToListAsync();

        return new PaginatedListDto<RequestLogToListDto>(totalCount, result);
    }
}