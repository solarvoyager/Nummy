using NummyShared.DTOs;
using NummyShared.DTOs.Domain;
using NummyShared.DTOs.Generic;
using NummyUi.Utils;

namespace NummyUi.Services;

public interface ILogService
{
    Task<PaginatedListDto<CodeLogToListDto>> GetCodeLogs(Guid? applicationId, GetCodeLogsDto dto);
    Task<IEnumerable<CodeLogToListDto>> GetCodeLogs(string traceIdentifier);
    Task<PaginatedListDto<RequestLogToListDto>> GetRequestLogs(Guid? applicationId, GetRequestLogsDto dto);
    Task<HttpLogDto> GetResponseLog(Guid httpLogId);
    Task<bool> DeleteCodeLogs(DeleteCodeLogsDto dto);
}

public class LogService(IHttpClientFactory clientFactory) : ILogService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyContants.ClientName);

    public async Task<PaginatedListDto<CodeLogToListDto>> GetCodeLogs(Guid? applicationId, GetCodeLogsDto dto)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(dto),
            Method = HttpMethod.Post,
            RequestUri =
                new Uri(
                    NummyContants.GetCodeLogsUrl +
                    (applicationId != null ? $"?applicationId={applicationId}" : string.Empty), UriKind.Relative)
        };

        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<PaginatedListDto<CodeLogToListDto>>();

        return result!;
    }

    public async Task<IEnumerable<CodeLogToListDto>> GetCodeLogs(string traceIdentifier)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(NummyContants.GetCodeLogsByTraceIdentifierUrl + $"/{traceIdentifier}",
                UriKind.Relative)
        };

        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<CodeLogToListDto>>();

        return result!;
    }

    public async Task<PaginatedListDto<RequestLogToListDto>> GetRequestLogs(Guid? applicationId, GetRequestLogsDto dto)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(dto),
            Method = HttpMethod.Post,
            RequestUri =
                new Uri(
                    NummyContants.GetRequestLogsUrl +
                    (applicationId != null ? $"?applicationId={applicationId}" : string.Empty), UriKind.Relative)
        };

        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<PaginatedListDto<RequestLogToListDto>>();

        return result!;
    }

    /*public async Task<PaginatedListDto<ResponseLogToListDto>> GetResponseLogs(GetResponseLogsDto dto, Guid? httpLogId)
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
    }*/

    public async Task<HttpLogDto> GetResponseLog(Guid httpLogId)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(NummyContants.GetResponseLogsUrl + $"/{httpLogId}", UriKind.Relative)
        };

        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<HttpLogDto>();

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