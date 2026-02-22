using NummyShared.DTOs;
using NummyShared.DTOs.Domain;
using NummyShared.DTOs.Generic;

namespace NummyUi.Services.Abstract;

public interface ILogService
{
    Task<PaginatedListDto<CodeLogToListDto>> GetCodeLogs(Guid? applicationId, GetCodeLogsDto dto);
    Task<IEnumerable<CodeLogToListDto>> GetCodeLogs(string traceIdentifier);
    Task<PaginatedListDto<RequestLogToListDto>> GetRequestLogs(Guid? applicationId, GetRequestLogsDto dto);
    Task<HttpLogDto> GetResponseLog(Guid httpLogId);
    Task<bool> DeleteCodeLogs(DeleteCodeLogsDto dto);
}
