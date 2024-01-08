using System.Text.Json;
using NummyUi.Dtos;

namespace NummyUi.Services;

public interface ILogService
{
    Task<IEnumerable<CodeLogToListDto>> GetCodeLogs(int pageIndex, int pageSize);
    Task<IEnumerable<RequestLogToListDto>> GetRequestLogs(int pageIndex, int pageSize);
    Task<IEnumerable<ResponseLogToListDto>> GetResponseLogs(int pageIndex, int pageSize);
}

public class LogService(IHttpClientFactory clientFactory) : ILogService
{
    private readonly HttpClient _client = clientFactory.CreateClient(NummyContants.ClientName);

    public async Task<IEnumerable<CodeLogToListDto>> GetCodeLogs(int pageIndex, int pageSize)
    {
        var response =
            await _client.GetFromJsonAsync<IEnumerable<CodeLogToListDto>>(NummyContants.GetCodeLogsUrl +
                                                                          $"?pageIndex={pageIndex}&pageSize={pageSize}");

        return response!;
    }

    public async Task<IEnumerable<RequestLogToListDto>> GetRequestLogs(int pageIndex, int pageSize)
    {
        var response =
            await _client.GetFromJsonAsync<IEnumerable<RequestLogToListDto>>(NummyContants.GetRequestLogsUrl +
                                                                             $"?pageIndex={pageIndex}&pageSize={pageSize}");

        return response!;
    }

    public async Task<IEnumerable<ResponseLogToListDto>> GetResponseLogs(int pageIndex, int pageSize)
    {
        var response =
            await _client.GetFromJsonAsync<IEnumerable<ResponseLogToListDto>>(NummyContants.GetResponseLogsUrl +
                                                                              $"?pageIndex={pageIndex}&pageSize={pageSize}");

        return response!;
    }
}