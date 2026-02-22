using NummyShared.DTOs;
using NummyShared.DTOs.Domain;

namespace NummyApi.Services.Abstract;

public interface IResponseLogService
{
    Task Add(ResponseLogToAddDto dto, CancellationToken cancellationToken = default);
    Task<HttpLogDto?> Get(Guid httpLogId, CancellationToken cancellationToken = default);
    Task<bool> Delete(DeleteResponseLogsDto dto, CancellationToken cancellationToken = default);
}
