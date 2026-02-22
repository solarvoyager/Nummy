using NummyShared.DTOs;
using NummyShared.DTOs.Domain;
using NummyShared.DTOs.Generic;

namespace NummyApi.Services.Abstract;

public interface IRequestLogService
{
    Task Add(RequestLogToAddDto dto, CancellationToken cancellationToken = default);
    Task<PaginatedListDto<RequestLogToListDto>> Get(Guid? applicationId, GetRequestLogsDto dto, CancellationToken cancellationToken = default);
    Task<bool> Delete(DeleteRequestLogsDto dto, CancellationToken cancellationToken = default);
}
