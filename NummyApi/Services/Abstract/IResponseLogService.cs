using NummyApi.Dtos;
using NummyApi.Dtos.Domain;
using NummyApi.Dtos.Generic;

namespace NummyApi.Services.Abstract;

public interface IResponseLogService
{
    Task Add(ResponseLogToAddDto dto);
    Task<PaginatedListDto<ResponseLogToListDto>> Get(GetResponseLogsDto dto);
    Task<bool> Delete(DeleteResponseLogsDto dto);
}