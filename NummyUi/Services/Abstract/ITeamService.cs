using NummyShared.DTOs;

namespace NummyUi.Services.Abstract;

public interface ITeamService
{
    Task<TeamToListDto?> Get(Guid id);
    Task<IEnumerable<TeamToListDto>> Get();
    Task<TeamToListDto> Add(string name, string description, IEnumerable<Guid> userIds, IEnumerable<Guid> applicationIds);
    Task<TeamToListDto?> Update(Guid id, string name, string description);
    Task Delete(Guid id);
}
