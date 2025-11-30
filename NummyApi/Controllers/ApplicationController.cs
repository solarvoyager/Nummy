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
    public async Task<ActionResult<List<ApplicationToListDto>>> Get()
    {
        var applications = await applicationService.GetAsync();
        return Ok(applications);
    }

    [HttpGet("stackType")]
    public async Task<ActionResult<List<ApplicationStackToListDto>>> GetStackType()
    {
        var stackTypes = await applicationService.GetStackTypeAsync();
        return Ok(stackTypes);
    }

    [HttpGet("healthCheckerUrl")]
    public async Task<ActionResult<List<ApplicationHealthCheckerUrlToListDto>>> GetHealthCheckerUrl()
    {
        var stackTypes = await applicationService.GetHealthCheckerUrlAsync();
        return Ok(stackTypes);
    }

    [HttpPut("isHealthy")]
    public async Task<ActionResult> UpdateIsHealthy([FromBody] List<ApplicationIsHealthyToUpdateDto> applications)
    {
        await applicationService.UpdateIsHealthyAsync(applications);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationToListDto>> Get(Guid id)
    {
        var application = await applicationService.GetAsync(id);
        return application == null ? NotFound() : Ok(application);
    }

    [HttpPost]
    public async Task<ActionResult<ApplicationToListDto>> Add(ApplicationToAddDto request)
    {
        var application = await applicationService.AddAsync(request);
        return Ok(application);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApplicationToListDto>> Update(Guid id, ApplicationToUpdateDto request)
    {
        var application = await applicationService.UpdateAsync(id, request);
        return application == null ? NotFound() : Ok(application);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await applicationService.DeleteAsync(id);
        return Ok();
    }
}