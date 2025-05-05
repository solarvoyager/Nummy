using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NummyApi.DataContext;
using NummyApi.Entitites;
using NummyApi.Helpers;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;

namespace NummyApi.Services.Concrete;

public class TeamService(NummyDataContext dataContext, IMapper mapper) : ITeamService
{
    public async Task<TeamToListDto?> GetAsync(Guid id)
    {
        var team = await dataContext.Teams
            .Include(t => t.TeamUsers)
            .ThenInclude(tu => tu.User)
            .Include(t => t.TeamApplications)
            .ThenInclude(ta => ta.Application)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (team == null)
            return null;

        var mapped = mapper.Map<TeamToListDto>(team);

        return mapped;
    }

    public async Task<IEnumerable<TeamToListDto>> GetAsync()
    {
        var teams = await dataContext.Teams
            .Include(t => t.TeamUsers)
            .ThenInclude(tu => tu.User!)
            .Include(t => t.TeamApplications)
            .ThenInclude(ta => ta.Application!)
            .OrderByDescending(t=> t.CreatedAt)
            .ToListAsync();

        var mappeds = mapper.Map<List<TeamToListDto>>(teams);

        return mappeds;
    }

    public async Task<TeamToListDto> AddAsync(TeamToAddDto dto)
    {
        var team = mapper.Map<Team>(dto);
        team.AvatarColorHex = UtilHelper.GenerateRandomColorHex();

        var added = await dataContext.Teams.AddAsync(team);
        await dataContext.SaveChangesAsync();
        
        await AddUsersToTeam(added.Entity.Id, dto.UserIds);
        await AddApplicationsToTeam(added.Entity.Id, dto.ApplicationIds);
        
        await dataContext.SaveChangesAsync();
        var mapped = mapper.Map<TeamToListDto>(added.Entity);

        return mapped;
    }

    

    public async Task<TeamToListDto?> UpdateAsync(Guid id, TeamToUpdateDto dto)
    {
        var team = await dataContext.Teams.FindAsync(id);
        if (team == null)
            return null;

        team.Name = dto.Name;
        team.Description = dto.Description;

        await dataContext.SaveChangesAsync();

        var mapped = mapper.Map<TeamToListDto>(team);

        return mapped;
    }

    public async Task DeleteAsync(Guid id)
    {
        var team = await dataContext.Teams.FindAsync(id);
        if (team == null) return;

        dataContext.Teams.Remove(team);
        await dataContext.SaveChangesAsync();
    }

    public async Task AddUserToTeamAsync(Guid teamId, Guid userId)
    {
        await AddUsersToTeam(teamId, [userId]);
        await dataContext.SaveChangesAsync();
    }

    public async Task AddApplicationToTeamAsync(Guid teamId, Guid applicationId)
    {
        await AddApplicationsToTeam(teamId, [applicationId]);
        await dataContext.SaveChangesAsync();
    }
    
    private async Task AddUsersToTeam(Guid teamId, IEnumerable<Guid> userIds)
    {
        foreach (var userId in userIds)
        {
            var teamUser = new TeamUser
            {
                TeamId = teamId,
                UserId = userId
            };

            await dataContext.TeamUsers.AddAsync(teamUser);
        }
    }
    
    private async Task AddApplicationsToTeam(Guid teamId, IEnumerable<Guid> applicationIds)
    {
        foreach (var applicationId in applicationIds)
        {
            var teamApplication = new TeamApplication
            {
                TeamId = teamId,
                ApplicationId = applicationId
            };

            await dataContext.TeamApplications.AddAsync(teamApplication);
        }
    }
}