using NummyShared.DTOs;
using NummyShared.DTOs.Domain;

namespace NummyApi.Services.Abstract;

public interface IApplicationService
{
    Task<ApplicationToListDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationToListDto>> GetAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationStackToListDto>> GetStackTypeAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ApplicationHealthCheckerUrlToListDto>> GetHealthCheckerUrlAsync(CancellationToken cancellationToken = default);
    Task UpdateIsHealthyAsync(List<ApplicationIsHealthyToUpdateDto> applications, CancellationToken cancellationToken = default);
    Task<ApplicationToListDto> AddAsync(ApplicationToAddDto dto, CancellationToken cancellationToken = default);
    Task<ApplicationToListDto?> UpdateAsync(Guid id, ApplicationToUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
