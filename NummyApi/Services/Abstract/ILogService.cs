using NummyApi.Dtos;

namespace NummyApi.Services.Abstract;

public interface ILogService
{
    Task AddRequestLog(RequestLogToAddDto dto);
    Task AddResponseLog(ResponseLogToAddDto dto);
    Task AddCodeLog(CodeLogToAddDto dto);
    
    Task<IEnumerable<RequestLogToListDto>> GetRequestLogs(int pageIndex, int pageSize);
    Task<IEnumerable<ResponseLogToListDto>> GetResponseLogs(int pageIndex, int pageSize);
    Task<IEnumerable<CodeLogToListDto>> GetCodeLogs(int pageIndex, int pageSize);
}