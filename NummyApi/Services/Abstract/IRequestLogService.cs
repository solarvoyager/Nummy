using NummyShared.DTOs;
using NummyShared.DTOs.Domain;
using NummyShared.DTOs.Generic;

namespace NummyApi.Services.Abstract;

public interface IRequestLogService
{
    Task Add(RequestLogToAddDto dto);
    Task<PaginatedListDto<RequestLogToListDto>> Get(GetRequestLogsDto dto);
    Task<bool> Delete(DeleteRequestLogsDto dto);
}