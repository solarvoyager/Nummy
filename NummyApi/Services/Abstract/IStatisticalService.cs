using NummyShared.DTOs.Domain;

namespace NummyApi.Services.Abstract;

public interface IStatisticalService
{
    Task<TotalCountsResponseDto> GetTotalCountsAsync();
}