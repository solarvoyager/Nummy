using Microsoft.AspNetCore.Mvc;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;
using NummyShared.DTOs.Domain;

namespace NummyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationController(IApplicationService applicationService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ApplicationToListDto>>> Get(CancellationToken cancellationToken)
    {
        var applications = await applicationService.GetAsync(cancellationToken);
        return Ok(applications);
    }

    [HttpGet("stackType")]
    public async Task<ActionResult<List<ApplicationStackToListDto>>> GetStackType(CancellationToken cancellationToken)
    {
        var stackTypes = await applicationService.GetStackTypeAsync(cancellationToken);
        return Ok(stackTypes);
    }

    [HttpGet("healthCheckerUrl")]
    public async Task<ActionResult<List<ApplicationHealthCheckerUrlToListDto>>> GetHealthCheckerUrl(CancellationToken cancellationToken)
    {
        var urls = await applicationService.GetHealthCheckerUrlAsync(cancellationToken);
        return Ok(urls);
    }

    [HttpPut("isHealthy")]
    public async Task<ActionResult> UpdateIsHealthy([FromBody] List<ApplicationIsHealthyToUpdateDto> applications, CancellationToken cancellationToken)
    {
        if (applications.Count == 0)
            return NoContent();

        await applicationService.UpdateIsHealthyAsync(applications, cancellationToken);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationToListDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var application = await applicationService.GetAsync(id, cancellationToken);
        return application == null ? NotFound() : Ok(application);
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationToListDto>> Add(ApplicationToAddDto request, CancellationToken cancellationToken)
    {
        var application = await applicationService.AddAsync(request, cancellationToken);
        return Ok(application);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApplicationToListDto>> Update(Guid id, ApplicationToUpdateDto request, CancellationToken cancellationToken)
    {
        var application = await applicationService.UpdateAsync(id, request, cancellationToken);
        return application == null ? NotFound() : Ok(application);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await applicationService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}
