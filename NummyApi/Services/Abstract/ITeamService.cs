using NummyShared.DTOs;

namespace NummyApi.Services.Abstract;

public interface ITeamService
{
    Task<TeamToListDto?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TeamToListDto>> GetAsync(CancellationToken cancellationToken = default);
    Task<TeamToListDto> AddAsync(TeamToAddDto dto, CancellationToken cancellationToken = default);
    Task<TeamToListDto?> UpdateAsync(Guid id, TeamToUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddUserToTeamAsync(Guid teamId, Guid userId, CancellationToken cancellationToken = default);
    Task AddApplicationToTeamAsync(Guid teamId, Guid applicationId, CancellationToken cancellationToken = default);
}
