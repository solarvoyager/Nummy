﻿using NummyShared.Dtos;
using NummyShared.Dtos.Domain;
using NummyShared.Dtos.Generic;

namespace NummyApi.Services.Abstract;

public interface IResponseLogService
{
    Task Add(ResponseLogToAddDto dto);
    Task<PaginatedListDto<ResponseLogToListDto>> Get(GetResponseLogsDto dto, string? httpLogId);
    Task<bool> Delete(DeleteResponseLogsDto dto);
}