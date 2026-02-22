using NummyShared.DTOs;
using NummyShared.DTOs.Domain;
using NummyShared.DTOs.Generic;
using NummyUi.Services.Abstract;
using NummyUi.Utils;

namespace NummyUi.Services;

public class LogService(IHttpClientFactory clientFactory) : ILogService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyConstants.ClientName);

    public async Task<PaginatedListDto<CodeLogToListDto>> GetCodeLogs(Guid? applicationId, GetCodeLogsDto dto)
    {
        var url = NummyConstants.GetCodeLogsUrl +
                  (applicationId != null ? $"?applicationId={applicationId}" : string.Empty);

        var response = await _client.PostAsJsonAsync(url, dto);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<PaginatedListDto<CodeLogToListDto>>();
        return result ?? new PaginatedListDto<CodeLogToListDto>(0, []);
    }

    public async Task<IEnumerable<CodeLogToListDto>> GetCodeLogs(string traceIdentifier)
    {
        var response = await _client.GetAsync(
            NummyConstants.GetCodeLogsByTraceIdentifierUrl + $"/{traceIdentifier}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<CodeLogToListDto>>();
        return result ?? [];
    }

    public async Task<PaginatedListDto<RequestLogToListDto>> GetRequestLogs(Guid? applicationId, GetRequestLogsDto dto)
    {
        var url = NummyConstants.GetRequestLogsUrl +
                  (applicationId != null ? $"?applicationId={applicationId}" : string.Empty);

        var response = await _client.PostAsJsonAsync(url, dto);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<PaginatedListDto<RequestLogToListDto>>();
        return result ?? new PaginatedListDto<RequestLogToListDto>(0, []);
    }

    public async Task<HttpLogDto> GetResponseLog(Guid httpLogId)
    {
        var response = await _client.GetAsync(NummyConstants.GetResponseLogsUrl + $"/{httpLogId}");
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<HttpLogDto>();
        return result ?? throw new InvalidOperationException($"HTTP log '{httpLogId}' response was empty.");
    }

    public async Task<bool> DeleteCodeLogs(DeleteCodeLogsDto dto)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(dto),
            Method = HttpMethod.Delete,
            RequestUri = new Uri(NummyConstants.DeleteCodeLogsUrl, UriKind.Relative)
        };

        var response = await _client.SendAsync(request);
        return response.IsSuccessStatusCode;
    }
}
