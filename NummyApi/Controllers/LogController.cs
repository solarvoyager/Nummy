using Microsoft.AspNetCore.Mvc;
using NummyApi.Dtos;
using NummyApi.Services.Abstract;

namespace NummyApi.Controllers;

[Route("api/[controller]")]
public class LogController(ILogService logService) : Controller
{
    [HttpPost("code/log")]
    public async Task<IActionResult> AddCodeLog(CodeLogToAddDto request)
    {
        await logService.AddCodeLog(request);
        return Ok();
    }

    [HttpPost("request/log")]
    public async Task<IActionResult> AddRequestLog(RequestLogToAddDto request)
    {
        await logService.AddRequestLog(request);
        return Ok();
    }

    [HttpPost("response/log")]
    public async Task<IActionResult> AddResponseLog(ResponseLogToAddDto request)
    {
        await logService.AddResponseLog(request);
        return Ok();
    }
}