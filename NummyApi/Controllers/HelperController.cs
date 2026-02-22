using Microsoft.AspNetCore.Mvc;
using NummyApi.Services.Abstract;
using NummyShared.DTOs.Domain;

namespace NummyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelperController(IDatabaseService databaseService) : ControllerBase
{
    [HttpGet("service-url")]
    public IActionResult GetServiceUrl()
    {
        var request = HttpContext.Request;
        var appUri = new Uri($"{request.Scheme}://{request.Host}{request.PathBase}");
        return Ok(new ServiceUrlResponseDto(appUri.AbsoluteUri));
    }

    [HttpGet("pending-migrations")]
    [Produces<List<string>>]
    public async Task<IActionResult> GetPendingMigrations(CancellationToken cancellationToken)
    {
        var data = await databaseService.GetPendingMigrations(cancellationToken);
        return Ok(data);
    }

    [HttpPut("apply-pending-migrations")]
    public async Task<IActionResult> ApplyPendingMigrations(CancellationToken cancellationToken)
    {
        await databaseService.ApplyPendingMigrations(cancellationToken);
        return Ok();
    }
}
