using Microsoft.AspNetCore.Mvc;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;

namespace NummyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController(ITeamService teamService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TeamToListDto>>> Get()
    {
        var teams = await teamService.GetAsync();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamToListDto>> Get(Guid id)
    {
        var team = await teamService.GetAsync(id);
        return team == null ? NotFound() : Ok(team);
    }

    [HttpPost]
    public async Task<ActionResult<TeamToListDto>> Add(TeamToAddDto request)
    {
        var team = await teamService.AddAsync(request);
        return Ok(team);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TeamToListDto>> Update(Guid id, TeamToUpdateDto request)
    {
        var team = await teamService.UpdateAsync(id, request);
        return team == null ? NotFound() : Ok(team);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(Guid id)
    {
        await teamService.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("{teamId}/users/{userId}")]
    public async Task<IActionResult> AddUserToTeam(Guid teamId, Guid userId)
    {
        await teamService.AddUserToTeamAsync(teamId, userId);
        return Ok();
    }


    [HttpPost("{teamId}/applications/{applicationId}")]
    public async Task<IActionResult> AddApplicationToTeam(Guid teamId, Guid applicationId)
    {
        await teamService.AddApplicationToTeamAsync(teamId, applicationId);
        return Ok();
    }
}