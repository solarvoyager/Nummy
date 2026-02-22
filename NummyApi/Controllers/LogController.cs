using Microsoft.AspNetCore.Mvc;
using NummyApi.Services.Abstract;
using NummyShared.DTOs;
using NummyShared.DTOs.Domain;

namespace NummyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogController(
    ICodeLogService codeLogService,
    IRequestLogService requestLogService,
    IResponseLogService responseLogService) : ControllerBase
{
    [HttpPost("add/code")]
    public async Task<IActionResult> AddCodeLog([FromBody] CodeLogToAddDto request, CancellationToken cancellationToken)
    {
        await codeLogService.Add(request, cancellationToken);
        return Ok();
    }

    [HttpPost("add/request")]
    public async Task<IActionResult> AddRequestLog([FromBody] RequestLogToAddDto request, CancellationToken cancellationToken)
    {
        await requestLogService.Add(request, cancellationToken);
        return Ok();
    }

    [HttpPost("add/response")]
    public async Task<IActionResult> AddResponseLog([FromBody] ResponseLogToAddDto request, CancellationToken cancellationToken)
    {
        await responseLogService.Add(request, cancellationToken);
        return Ok();
    }

    [HttpPost("get/code")]
    public async Task<IActionResult> GetCodeLogs([FromQuery] Guid? applicationId, [FromBody] GetCodeLogsDto request, CancellationToken cancellationToken)
    {
        var response = await codeLogService.Get(applicationId, request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("get/request")]
    public async Task<IActionResult> GetRequestLogs([FromQuery] Guid? applicationId, [FromBody] GetRequestLogsDto request, CancellationToken cancellationToken)
    {
        var response = await requestLogService.Get(applicationId, request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("get/response/{httpLogId:guid}")]
    public async Task<IActionResult> GetResponseLog([FromRoute] Guid httpLogId, CancellationToken cancellationToken)
    {
        var response = await responseLogService.Get(httpLogId, cancellationToken);
        return Ok(response);
    }

    [HttpGet("get/code/{traceIdentifier}")]
    public async Task<IActionResult> GetCodeLogs([FromRoute] string traceIdentifier, CancellationToken cancellationToken)
    {
        var response = await codeLogService.Get(traceIdentifier, cancellationToken);
        return Ok(response);
    }

    [HttpDelete("delete/code")]
    public async Task<IActionResult> DeleteCodeLogs([FromBody] DeleteCodeLogsDto request, CancellationToken cancellationToken)
    {
        var response = await codeLogService.Delete(request, cancellationToken);
        return response ? Ok() : Problem();
    }

    [HttpDelete("delete/request")]
    public async Task<IActionResult> DeleteRequestLogs([FromBody] DeleteRequestLogsDto request, CancellationToken cancellationToken)
    {
        var response = await requestLogService.Delete(request, cancellationToken);
        return response ? Ok() : Problem();
    }

    [HttpDelete("delete/response")]
    public async Task<IActionResult> DeleteResponseLogs([FromBody] DeleteResponseLogsDto request, CancellationToken cancellationToken)
    {
        var response = await responseLogService.Delete(request, cancellationToken);
        return response ? Ok() : Problem();
    }
}
