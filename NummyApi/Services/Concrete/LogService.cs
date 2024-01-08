using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Dtos;
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

    public async Task<PaginatedListDto<CodeLogToListDto>> GetCodeLogs(int pageIndex, int pageSize)
    {
        var skip = (pageIndex - 1) * pageSize;

        var data = await dataContext.CodeLogs
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await dataContext.CodeLogs.CountAsync();

        var mapped = mapper.Map<IEnumerable<CodeLogToListDto>>(data);

        return new PaginatedListDto<CodeLogToListDto>(totalCount, mapped);
    }
}