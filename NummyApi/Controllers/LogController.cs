using Microsoft.AspNetCore.Mvc;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;
using NummyShared.DTOs.Domain;

namespace NummyApi.Controllers;

[Route("api/[controller]")]
public class LogController(
    ICodeLogService codeLogService,
    IRequestLogService requestLogService,
    IResponseLogService responseLogService) : Controller
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
    public async Task<IActionResult> GetCodeLogs([FromQuery] Guid? applicationId, [FromBody] GetCodeLogsDto request)
    {
        var response = await codeLogService.Get(applicationId, request);

        return Ok(response);
    }

    [HttpPost("get/request")]
    public async Task<IActionResult> GetRequestLogs([FromQuery] Guid? applicationId, [FromBody] GetRequestLogsDto request)
    {
        var response = await requestLogService.Get(applicationId, request);

        return Ok(response);
    }

    /*[HttpPost("get/response")]
    public async Task<IActionResult> GetResponseLogs([FromBody] GetResponseLogsDto request, [FromQuery] Guid? httpLogId)
    {
        var response = await responseLogService.Get(request, httpLogId);

        return Ok(response);
    }*/

    [HttpGet("get/response/{httpLogId:guid}")]
    public async Task<IActionResult> GetResponseLog([FromRoute] Guid httpLogId)
    {
        var response = await responseLogService.Get(httpLogId);

        return Ok(response);
    }

    [HttpGet("get/code/{traceIdentifier}")]
    public async Task<IActionResult> GetCodeLogs([FromRoute] string traceIdentifier)
    {
        var response = await codeLogService.Get(traceIdentifier);
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