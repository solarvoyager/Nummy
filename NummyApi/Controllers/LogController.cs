using Microsoft.AspNetCore.Mvc;
using NummyApi.Dtos;
using NummyApi.Dtos.Domain;
using NummyApi.Services.Abstract;

namespace NummyApi.Controllers;

[Route("api/[controller]")]
public class LogController(ICodeLogService codeLogService, IRequestLogService requestLogService, IResponseLogService responseLogService) : Controller
{
    [HttpPost("add/code")]
    public async Task<IActionResult> AddCodeLog([FromBody] CodeLogToAddDto request)
    {
        await codeLogService.Add(request);
        return Ok();
    }

    [HttpPost("add/request")]
    public async Task<IActionResult> AddRequestLog([FromBody] RequestLogToAddDto request)
    {
        await requestLogService.Add(request);
        return Ok();
    }

    [HttpPost("add/response")]
    public async Task<IActionResult> AddResponseLog([FromBody] ResponseLogToAddDto request)
    {
        await responseLogService.Add(request);
        return Ok();
    }

    [HttpPost("get/code")]
    public async Task<IActionResult> GetCodeLogs([FromBody] GetCodeLogsDto request)
    {
        var response = await codeLogService.Get(request);

        return Ok(response);
    }

    [HttpPost("get/request")]
    public async Task<IActionResult> GetRequestLogs([FromBody] GetRequestLogsDto request)
    {
        var response = await requestLogService.Get(request);

        return Ok(response);
    }

    [HttpPost("get/response")]
    public async Task<IActionResult> GetResponseLogs([FromBody] GetResponseLogsDto request)
    {
        var response = await responseLogService.Get(request);

        return Ok(response);
    }
    
    [HttpDelete("delete/code")]
    public async Task<IActionResult> DeleteCodeLogs([FromBody] DeleteCodeLogsDto request)
    {
        var response = await codeLogService.Delete(request);

        return response ? Ok() : Problem();
    }

    [HttpDelete("delete/request")]
    public async Task<IActionResult> DeleteRequestLogs([FromBody] DeleteRequestLogsDto request)
    {
        var response = await requestLogService.Delete(request);

        return response ? Ok() : Problem();
    }

    [HttpDelete("delete/response")]
    public async Task<IActionResult> DeleteResponseLogs([FromBody] DeleteResponseLogsDto request)
    {
        var response = await responseLogService.Delete(request);

        return response ? Ok() : Problem();
    }
}