using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Entitites;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;
using NummyShared.DTOs.Domain;

namespace NummyApi.Services.Concrete;

public class ResponseLogService(NummyDataContext dataContext, IMapper mapper) : IResponseLogService
{
    public async Task<bool> Delete(DeleteResponseLogsDto dto)
    {
        await dataContext.ResponseLogs
            .Where(l => dto.Ids.Contains(l.Id))
            .ExecuteDeleteAsync();

        return true;
    }

    public async Task Add(ResponseLogToAddDto dto)
    {
        var mapped = mapper.Map<ResponseLog>(dto);

        await dataContext.AddAsync(mapped);
        await dataContext.SaveChangesAsync();
    }

    /*public async Task<PaginatedListDto<ResponseLogToListDto>> Get(GetResponseLogsDto dto, Guid? httpLogId)
    {
        var skip = (dto.PageIndex - 1) * dto.PageSize;

        var query = dataContext.ResponseLogs.AsQueryable();

        if (httpLogId.HasValue)
        {
            query = query.Where(l => l.HttpLogId == httpLogId);
        }

        if (!string.IsNullOrWhiteSpace(dto.Query))
            query = query.Where(l =>
                EF.Functions.Like(l.Body.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(l.StatusCode.ToString().ToLower(), $"%{dto.Query.ToLower()}%"));

        var totalCount = await query.CountAsync();

        if (dto.SortType is not null && dto.SortOrder is not null)
            query = dto.SortType switch
            {
                ResponseLogSortType.StatusCode => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.StatusCode)
                    : query.OrderBy(q => q.StatusCode),
                _ => query
            };

        query = query
            .Skip(skip)
            .Take(dto.PageSize);

        var mapped = mapper.Map<IEnumerable<ResponseLogToListDto>>(await query.ToListAsync());

        return new PaginatedListDto<ResponseLogToListDto>(totalCount, mapped);
    }*/

    public async Task<HttpLogDto?> Get(Guid httpLogId)
    {
        var request = await dataContext.RequestLogs
            .Include(l => l.Headers)
            .FirstOrDefaultAsync(l => l.HttpLogId == httpLogId);
        if (request == null) return null;

        var response = await dataContext.ResponseLogs
            .Include(l => l.Headers)
            .FirstOrDefaultAsync(l => l.HttpLogId == httpLogId);

        var data = new HttpLogDto
        (
            response?.Id ?? Guid.Empty, // or whatever default you want
            request.HttpLogId,
            request.Body,
            request.Headers.Select(h => new HeaderDto(h.Key, h.Value)).ToList(),
            response?.Body,
            response?.Headers.Select(h => new HeaderDto(h.Key, h.Value)).ToList(),
            response?.StatusCode,
            response?.DurationMs,
            response?.CreatedAt
        );

        return data;
    }
}