using NummyShared.DTOs;

namespace NummyUi.Services.Abstract;

public interface IApplicationService
{
    Task<ApplicationToListDto?> Get(Guid id);
    Task<IEnumerable<ApplicationToListDto>> Get();
    Task<IEnumerable<ApplicationStackToListDto>> GetStackType();
    Task<ApplicationToListDto> Add(string name, string description, string? healthCheckerUrl, Guid stackTypeId);
    Task<ApplicationToListDto?> Update(Guid id, string name, string description, string? healthCheckerUrl, Guid stackTypeId);
    Task Delete(Guid id);
}
