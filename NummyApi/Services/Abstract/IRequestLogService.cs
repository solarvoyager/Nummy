using NummyApi.Dtos;
using NummyApi.Dtos.Domain;
using NummyApi.Dtos.Generic;

namespace NummyApi.Services.Abstract;

public interface IRequestLogService
{
    Task Add(RequestLogToAddDto dto);
    Task<PaginatedListDto<RequestLogToListDto>> Get(GetRequestLogsDto dto);
    Task<bool> Delete(DeleteRequestLogsDto dto);
}