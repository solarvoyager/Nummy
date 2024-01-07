using AutoMapper;
using NummyApi.Dtos;
using NummyApi.Entitites;
using NummyApi.Services.Abstract;

namespace NummyApi.Services.Concrete;

public class LogService(DataContext.DataContext dataContext, IMapper mapper) : ILogService
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
}