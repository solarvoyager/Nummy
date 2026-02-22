using Microsoft.AspNetCore.Mvc;
using NummyApi.Services.Abstract;

namespace NummyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticalController(IStatisticalService statisticalService) : ControllerBase
{
    [HttpGet("total")]
    public async Task<IActionResult> GetTotalCounts(CancellationToken cancellationToken)
    {
        var response = await statisticalService.GetTotalCountsAsync(cancellationToken);
        return Ok(response);
    }
}
