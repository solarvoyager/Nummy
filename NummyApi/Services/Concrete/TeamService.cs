using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Entitites;
using NummyApi.Helpers;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;

namespace NummyApi.Services.Concrete;

public class TeamService(NummyDataContext dataContext, IMapper mapper) : ITeamService
{
    public async Task<TeamToListDto?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var team = await dataContext.Teams
            .Include(t => t.TeamUsers)
            .ThenInclude(tu => tu.User)
            .Include(t => t.TeamApplications)
            .ThenInclude(ta => ta.Application)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (team == null)
            return null;

        return mapper.Map<TeamToListDto>(team);
    }

    public async Task<IEnumerable<TeamToListDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        var teams = await dataContext.Teams
            .Include(t => t.TeamUsers)
            .ThenInclude(tu => tu.User!)
            .Include(t => t.TeamApplications)
            .ThenInclude(ta => ta.Application!)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<TeamToListDto>>(teams);
    }

    public async Task<TeamToListDto> AddAsync(TeamToAddDto dto, CancellationToken cancellationToken = default)
    {
        var team = mapper.Map<Team>(dto);
        team.AvatarColorHex = UtilHelper.GenerateRandomColorHex();

        var added = await dataContext.Teams.AddAsync(team, cancellationToken);
        await dataContext.SaveChangesAsync(cancellationToken);

        await AddUsersToTeam(added.Entity.Id, dto.UserIds, cancellationToken);
        await AddApplicationsToTeam(added.Entity.Id, dto.ApplicationIds, cancellationToken);

        await dataContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<TeamToListDto>(added.Entity);
    }

    public async Task<TeamToListDto?> UpdateAsync(Guid id, TeamToUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var team = await dataContext.Teams.FindAsync([id], cancellationToken);
        if (team == null)
            return null;

        team.Name = dto.Name;
        team.Description = dto.Description;

        await dataContext.SaveChangesAsync(cancellationToken);

        return mapper.Map<TeamToListDto>(team);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var team = await dataContext.Teams.FindAsync([id], cancellationToken);
        if (team == null) return;

        dataContext.Teams.Remove(team);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddUserToTeamAsync(Guid teamId, Guid userId, CancellationToken cancellationToken = default)
    {
        await AddUsersToTeam(teamId, [userId], cancellationToken);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddApplicationToTeamAsync(Guid teamId, Guid applicationId, CancellationToken cancellationToken = default)
    {
        await AddApplicationsToTeam(teamId, [applicationId], cancellationToken);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    private async Task AddUsersToTeam(Guid teamId, IEnumerable<Guid> userIds, CancellationToken cancellationToken)
    {
        var teamUsers = userIds.Select(uid => new TeamUser { TeamId = teamId, UserId = uid });
        await dataContext.TeamUsers.AddRangeAsync(teamUsers, cancellationToken);
    }

    private async Task AddApplicationsToTeam(Guid teamId, IEnumerable<Guid> applicationIds, CancellationToken cancellationToken)
    {
        var teamApplications = applicationIds.Select(appId => new TeamApplication { TeamId = teamId, ApplicationId = appId });
        await dataContext.TeamApplications.AddRangeAsync(teamApplications, cancellationToken);
    }
}
