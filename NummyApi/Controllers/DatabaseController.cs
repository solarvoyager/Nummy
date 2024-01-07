using Microsoft.AspNetCore.Mvc;
using NummyApi.Services.Abstract;

namespace NummyApi.Controllers;

[Route("api/[controller]")]
public class DatabaseController(IDatabaseService databaseService) : Controller
{
    [HttpGet("migrations/pending")]
    [Produces<List<string>>]
    public async Task<IActionResult> GetPendingMigrations()
    {
        var data = await databaseService.GetPendingMigrations();

        return Ok(data);
    }

    [HttpPut("migrations/pending/apply")]
    public async Task<IActionResult> ApplyPendingMigrations()
    {
        var data = await databaseService.GetPendingMigrations();

        return Ok(data);
    }
}