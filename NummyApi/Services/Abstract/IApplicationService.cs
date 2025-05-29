using NummyShared.DTOs;
using NummyShared.DTOs.Enums;

namespace NummyApi.Services.Abstract;

public interface IApplicationService
{
    Task<ApplicationToListDto?> GetAsync(Guid id);
    Task<IEnumerable<ApplicationToListDto>> GetAsync();
    Task<IEnumerable<ApplicationStackToListDto>> GetStackTypeAsync();
    Task<ApplicationToListDto> AddAsync(ApplicationToAddDto dto);
    Task<ApplicationToListDto?> UpdateAsync(Guid id, ApplicationToUpdateDto dto);
    Task DeleteAsync(Guid id);
}