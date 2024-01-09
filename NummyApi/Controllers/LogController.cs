using Microsoft.AspNetCore.Mvc;
using NummyApi.Dtos;
using NummyApi.Dtos.Domain;
using NummyApi.Services.Abstract;

namespace NummyApi.Controllers;

[Route("api/[controller]")]
public class LogController(ILogService logService) : Controller
{
    [HttpPost("code")]
    public async Task<IActionResult> AddCodeLog([FromBody] CodeLogToAddDto request)
    {
        await logService.AddCodeLog(request);
        return Ok();
    }

    [HttpPost("request")]
    public async Task<IActionResult> AddRequestLog([FromBody] RequestLogToAddDto request)
    {
        await logService.AddRequestLog(request);
        return Ok();
    }

    [HttpPost("response")]
    public async Task<IActionResult> AddResponseLog([FromBody] ResponseLogToAddDto request)
    {
        await logService.AddResponseLog(request);
        return Ok();
    }

    [HttpPost("get/code")]
    public async Task<IActionResult> GetCodeLogs([FromBody] GetCodeLogsRequestDto request)
    {
        var response = await logService.GetCodeLogs(request);

        return Ok(response);
    }

    [HttpGet("request")]
    public async Task<IActionResult> GetRequestLogs([FromQuery] int pageIndex, [FromQuery] int pageSize)
    {
        var response = await logService.GetRequestLogs(pageIndex, pageSize);

        return Ok(response);
    }

    [HttpGet("response")]
    public async Task<IActionResult> GetResponseLogs([FromQuery] int pageIndex, [FromQuery] int pageSize)
    {
        var response = await logService.GetResponseLogs(pageIndex, pageSize);

        return Ok(response);
    }
}