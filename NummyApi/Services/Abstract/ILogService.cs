using NummyApi.Dtos;

namespace NummyApi.Services.Abstract;

public interface ILogService
{
    Task AddRequestLog(RequestLogToAddDto dto);
    Task AddResponseLog(ResponseLogToAddDto dto);
    Task AddCodeLog(CodeLogToAddDto dto);
}