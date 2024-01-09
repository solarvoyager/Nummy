using System.Text.Json;
using System.Text.Json.Serialization;
using NummyUi.Dtos;
using NummyUi.Dtos.Domain;
using NummyUi.Dtos.Generic;

namespace NummyUi.Services;

public interface ILogService
{
    Task<PaginatedListDto<CodeLogToListDto>> GetCodeLogs(GetCodeLogsRequestDto dto);
    Task<PaginatedListDto<RequestLogToListDto>> GetRequestLogs(int pageIndex, int pageSize);
    Task<PaginatedListDto<ResponseLogToListDto>> GetResponseLogs(int pageIndex, int pageSize);
}

public class LogService(IHttpClientFactory clientFactory) : ILogService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyContants.ClientName);

    public async Task<PaginatedListDto<CodeLogToListDto>> GetCodeLogs(GetCodeLogsRequestDto dto)
    {
        var requestContent = JsonContent.Create(dto);
        
        var response =
            await _client.PostAsync(NummyContants.GetCodeLogsUrl, requestContent);

        var result = await response.Content.ReadFromJsonAsync<PaginatedListDto<CodeLogToListDto>>();

        return result!;
    }

    public async Task<PaginatedListDto<RequestLogToListDto>> GetRequestLogs(int pageIndex, int pageSize)
    {
        var response =
            await _client.GetFromJsonAsync<PaginatedListDto<RequestLogToListDto>>(NummyContants.GetRequestLogsUrl +
                $"?pageIndex={pageIndex}&pageSize={pageSize}");

        return response!;
    }

    public async Task<PaginatedListDto<ResponseLogToListDto>> GetResponseLogs(int pageIndex, int pageSize)
    {
        var response =
            await _client.GetFromJsonAsync<PaginatedListDto<ResponseLogToListDto>>(NummyContants.GetResponseLogsUrl +
                $"?pageIndex={pageIndex}&pageSize={pageSize}");

        return response!;
    }
}