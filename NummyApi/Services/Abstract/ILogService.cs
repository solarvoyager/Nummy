using NummyApi.Dtos;
using NummyApi.Dtos.Domain;
using NummyApi.Dtos.Generic;

namespace NummyApi.Services.Abstract;

public interface ILogService
{
    Task AddRequestLog(RequestLogToAddDto dto);
    Task AddResponseLog(ResponseLogToAddDto dto);
    Task AddCodeLog(CodeLogToAddDto dto);

    Task<PaginatedListDto<RequestLogToListDto>> GetRequestLogs(int pageIndex, int pageSize);
    Task<PaginatedListDto<ResponseLogToListDto>> GetResponseLogs(int pageIndex, int pageSize);
    Task<PaginatedListDto<CodeLogToListDto>> GetCodeLogs(GetCodeLogsRequestDto dto);
}