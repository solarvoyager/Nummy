using NummyShared.DTOs;
using NummyShared.DTOs.Domain;
using NummyShared.DTOs.Generic;

namespace NummyApi.Services.Abstract;

public interface ICodeLogService
{
    Task Add(CodeLogToAddDto dto, CancellationToken cancellationToken = default);
    Task<PaginatedListDto<CodeLogToListDto>> Get(Guid? applicationId, GetCodeLogsDto dto, CancellationToken cancellationToken = default);
    Task<bool> Delete(DeleteCodeLogsDto dto, CancellationToken cancellationToken = default);
    Task<IEnumerable<CodeLogToListDto>> Get(string traceIdentifier, CancellationToken cancellationToken = default);
}
