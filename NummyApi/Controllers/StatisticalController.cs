using Microsoft.AspNetCore.Mvc;
using NummyApi.Services.Abstract;

namespace NummyApi.Controllers;

[Route("api/[controller]")]
public class StatisticalController(
    IStatisticalService statisticalService) : Controller
{
    [HttpGet("total")]
    public async Task<IActionResult> GetTotalCounts()
    {
        var response = await statisticalService.GetTotalCountsAsync();
        
        return Ok(response);
    }

    
}