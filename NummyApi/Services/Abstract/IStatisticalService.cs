using NummyApi.Dtos;
using NummyApi.Dtos.Domain;
using NummyApi.Dtos.Generic;

namespace NummyApi.Services.Abstract;

public interface IStatisticalService
{
    Task<TotalCountsResponseDto> GetTotalCountsAsync();
}