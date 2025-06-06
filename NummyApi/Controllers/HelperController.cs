using Microsoft.AspNetCore.Mvc;
using NummyApi.Services.Abstract;
using NummyShared.DTOs.Domain;

namespace NummyApi.Controllers;

[Route("api/[controller]")]
public class HelperController(IDatabaseService databaseService) : Controller
{
    [HttpGet("service-url")]
    public IActionResult GetServiceUrl()
    {
        var request = HttpContext.Request;
        var appUri = new Uri($"{request.Scheme}://{request.Host}{request.PathBase}");
        var response = new ServiceUrlResponseDto(appUri.AbsoluteUri);
        
        return Ok(response);
    }
    
    [HttpGet("pending-migrations")]
    [Produces<List<string>>]
    public async Task<IActionResult> GetPendingMigrations()
    {
        var data = await databaseService.GetPendingMigrations();

        return Ok(data);
    }

    [HttpPut("apply-pending-migrations")]
    public async Task<IActionResult> ApplyPendingMigrations()
    {
        await databaseService.ApplyPendingMigrations();

        return Ok();
    }
}