using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Entitites;
using NummyApi.Services.Abstract;
using NummyShared.Dtos;
using NummyShared.Dtos.Domain;
using NummyShared.Dtos.Generic;

namespace NummyApi.Services.Concrete;

public class ResponseLogService(NummyDataContext dataContext, IMapper mapper) : IResponseLogService
{
    public async Task Add(ResponseLogToAddDto dto)
    {
        var mapped = mapper.Map<ResponseLog>(dto);

        await dataContext.AddAsync(mapped);
        await dataContext.SaveChangesAsync();
    }

    public async Task<PaginatedListDto<ResponseLogToListDto>> Get(GetResponseLogsDto? dto, string? httpLogId)
    {
        var skip = (dto.PageIndex - 1) * dto.PageSize;

        var query = dataContext.ResponseLogs.AsQueryable();

        query = query.Where(l => l.HttpLogId.ToString() == httpLogId);

        if (!string.IsNullOrWhiteSpace(dto.Query))
            query = query.Where(l =>
                EF.Functions.Like(l.Body.ToLower(), $"%{dto.Query.ToLower()}%") ||
                EF.Functions.Like(l.StatusCode.ToString().ToLower(), $"%{dto.Query.ToLower()}%"));

        var totalCount = await query.CountAsync();

        if (dto.SortType is not null && dto.SortOrder is not null)
            query = dto.SortType switch
            {
                ResponseLogSortType.Body => dto.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(q => q.Body)
                    : query.OrderBy(q => q.Body),
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
    }

    public async Task<bool> Delete(DeleteResponseLogsDto dto)
    {
        await dataContext.ResponseLogs.Where(l => dto.Ids.Contains(l.Id)).ExecuteDeleteAsync();

        return true;
    }
}