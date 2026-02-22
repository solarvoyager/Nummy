using Microsoft.AspNetCore.Mvc;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;

namespace NummyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController(ITeamService teamService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TeamToListDto>>> Get(CancellationToken cancellationToken)
    {
        var teams = await teamService.GetAsync(cancellationToken);
        return Ok(teams);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamToListDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var team = await teamService.GetAsync(id, cancellationToken);
        return team == null ? NotFound() : Ok(team);
    }

    [HttpPost]
    public async Task<ActionResult<TeamToListDto>> Add(TeamToAddDto request, CancellationToken cancellationToken)
    {
        var team = await teamService.AddAsync(request, cancellationToken);
        return Ok(team);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TeamToListDto>> Update(Guid id, TeamToUpdateDto request, CancellationToken cancellationToken)
    {
        var team = await teamService.UpdateAsync(id, request, cancellationToken);
        return team == null ? NotFound() : Ok(team);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(Guid id, CancellationToken cancellationToken)
    {
        await teamService.DeleteAsync(id, cancellationToken);
        return Ok();
    }

    [HttpPost("{teamId}/users/{userId}")]
    public async Task<IActionResult> AddUserToTeam(Guid teamId, Guid userId, CancellationToken cancellationToken)
    {
        await teamService.AddUserToTeamAsync(teamId, userId, cancellationToken);
        return Ok();
    }

    [HttpPost("{teamId}/applications/{applicationId}")]
    public async Task<IActionResult> AddApplicationToTeam(Guid teamId, Guid applicationId, CancellationToken cancellationToken)
    {
        await teamService.AddApplicationToTeamAsync(teamId, applicationId, cancellationToken);
        return Ok();
    }
}
