using NummyShared.Dtos;
using NummyShared.Dtos.Domain;
using NummyShared.Dtos.Generic;
using NummyUi.Utils;

namespace NummyUi.Services;

public interface ILogService
{
    Task<PaginatedListDto<CodeLogToListDto>> GetCodeLogs(GetCodeLogsDto dto);
    Task<PaginatedListDto<RequestLogToListDto>> GetRequestLogs(GetRequestLogsDto dto, Guid? httpLogId);
    //Task<PaginatedListDto<ResponseLogToListDto>> GetResponseLogs(GetResponseLogsDto dto, Guid? httpLogId);
    Task<ResponseLogDto> GetResponseLog(Guid httpLogId);
    
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
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<PaginatedListDto<CodeLogToListDto>>();

        return result!;
    }

    public async Task<PaginatedListDto<RequestLogToListDto>> GetRequestLogs(GetRequestLogsDto dto, Guid? httpLogId)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(dto),
            Method = HttpMethod.Post,
            
            RequestUri = new Uri(NummyContants.GetRequestLogsUrl + $"?httpLogId={httpLogId}", UriKind.Relative)
        };
        
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        
        var result = await response.Content.ReadFromJsonAsync<PaginatedListDto<RequestLogToListDto>>();

        return result!;
    }

    public async Task<PaginatedListDto<ResponseLogToListDto>> GetResponseLogs(GetResponseLogsDto dto, Guid? httpLogId)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(dto),
            Method = HttpMethod.Post,
            RequestUri = new Uri(NummyContants.GetResponseLogUrl + $"?httpLogId={httpLogId}", UriKind.Relative)
        };
        
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<PaginatedListDto<ResponseLogToListDto>>();

        return result!;
    }

    public async Task<ResponseLogDto> GetResponseLog(Guid httpLogId)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(NummyContants.GetResponseLogsUrl + $"?httpLogId={httpLogId}", UriKind.Relative)
        };
        
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ResponseLogDto>();

        return result!;
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