using NummyShared.DTOs;

namespace NummyApi.Services.Abstract;

public interface ITeamService
{
    Task<TeamToListDto?> GetAsync(Guid id);
    Task<IEnumerable<TeamToListDto>> GetAsync();
    Task<TeamToListDto> AddAsync(TeamToAddDto dto);
    Task<TeamToListDto?> UpdateAsync(Guid id, TeamToUpdateDto dto);
    Task DeleteAsync(Guid id);
    Task AddUserToTeamAsync(Guid teamId, Guid userId);
}