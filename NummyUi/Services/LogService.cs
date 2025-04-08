using NummyShared.Dtos;
using NummyShared.Dtos.Domain;
using NummyShared.Dtos.Generic;
using NummyUi.Utils;

namespace NummyUi.Services;

public interface ILogService
{
    Task<PaginatedListDto<CodeLogToListDto>> GetCodeLogs(GetCodeLogsDto dto);
    Task<PaginatedListDto<RequestLogToListDto>> GetRequestLogs(int pageIndex, int pageSize);
    Task<PaginatedListDto<ResponseLogToListDto>> GetResponseLogs(int pageIndex, int pageSize);
    
    Task<bool> DeleteCodeLogs(DeleteCodeLogsDto dto);
}

public class LogService(IHttpClientFactory clientFactory) : ILogService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyContants.ClientName);

    public async Task<PaginatedListDto<CodeLogToListDto>> GetCodeLogs(GetCodeLogsDto dto)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(dto),
            Method = HttpMethod.Post,
            RequestUri = new Uri(NummyContants.GetCodeLogsUrl, UriKind.Relative)
        };
        
        var response = await _client.SendAsync(request);

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

    public async Task<bool> DeleteCodeLogs(DeleteCodeLogsDto dto)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(dto),
            Method = HttpMethod.Delete,
            RequestUri = new Uri(NummyContants.DeleteCodeLogsUrl, UriKind.Relative)
        };
        
        var response = await _client.SendAsync(request);
        
        return response.IsSuccessStatusCode;
    }
}