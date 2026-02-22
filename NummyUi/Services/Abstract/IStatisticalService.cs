using NummyShared.DTOs.Domain;

namespace NummyUi.Services.Abstract;

public interface IStatisticalService
{
    Task<TotalCountsResponseDto> GetTotalCounts();
}
