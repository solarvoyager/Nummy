using NummyShared.Dtos;
using NummyShared.Dtos.Domain;
using NummyShared.Dtos.Generic;

namespace NummyApi.Services.Abstract;

public interface IRequestLogService
{
    Task Add(RequestLogToAddDto dto);
    Task<PaginatedListDto<RequestLogToListDto>> Get(GetRequestLogsDto dto, Guid? httpLogId);
    Task<bool> Delete(DeleteRequestLogsDto dto);
}