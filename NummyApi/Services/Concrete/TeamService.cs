using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NummyApi.DataContext;
using NummyApi.Entitites;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;

namespace NummyApi.Services.Concrete;

public class TeamService(NummyDataContext dataContext, IMapper mapper) : ITeamService
{
    public async Task<TeamToListDto?> GetAsync(Guid id)
    {
        var team = await dataContext.Teams.FindAsync(id);
        if (team == null)
            return null;

        var mapped = mapper.Map<TeamToListDto>(team);

        return mapped;
    }

    public async Task<IEnumerable<TeamToListDto>> GetAsync()
    {
        var teams = await dataContext.Teams.ToListAsync();

        var mappeds = mapper.Map<List<TeamToListDto>>(teams);

        return mappeds;
    }

    public async Task<TeamToListDto> AddAsync(TeamToAddDto dto)
    {
        var team = mapper.Map<Team>(dto);

        await dataContext.Teams.AddAsync(team);
        await dataContext.SaveChangesAsync();

        var mapped = mapper.Map<TeamToListDto>(team);

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
        var teamUser = new TeamUser
        {
            TeamId = teamId,
            UserId = userId
        };

        dataContext.TeamUsers.Add(teamUser);
        await dataContext.SaveChangesAsync();
    }
}