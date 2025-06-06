using NummyShared.DTOs;
using NummyShared.DTOs.Domain;
using NummyShared.DTOs.Generic;

namespace NummyApi.Services.Abstract;

public interface ICodeLogService
{
    Task Add(CodeLogToAddDto dto);
    Task<PaginatedListDto<CodeLogToListDto>> Get(Guid? applicationId, GetCodeLogsDto dto);
    Task<bool> Delete(DeleteCodeLogsDto dto);
    Task<IEnumerable<CodeLogToListDto>> Get(string traceIdentifier);
}