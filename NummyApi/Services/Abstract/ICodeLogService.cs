using NummyApi.Dtos;
using NummyApi.Dtos.Domain;
using NummyApi.Dtos.Generic;

namespace NummyApi.Services.Abstract;

public interface ICodeLogService
{
    Task Add(CodeLogToAddDto dto);
    Task<PaginatedListDto<CodeLogToListDto>> Get(GetCodeLogsDto dto);
    Task<bool> Delete(DeleteCodeLogsDto dto);
}