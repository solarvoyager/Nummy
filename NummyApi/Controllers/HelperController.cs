using Microsoft.AspNetCore.Mvc;
using NummyShared.DTOs.Domain;

namespace NummyApi.Controllers;

[Route("api/[controller]")]
public class HelperController : Controller
{
    [HttpGet("dsn")]
    public IActionResult GetDsn()
    {
        var request = HttpContext.Request;
        var appUrl = request.Scheme + "://" + request.Host + request.PathBase;
        return Ok(new DsnResponseDto(appUrl));
    }
}