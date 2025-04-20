using AntDesign;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components;
using NummyShared.Dtos;
using NummyShared.Dtos.Domain;
using NummyShared.Dtos.Enums;

namespace NummyUi.Pages.HttpLogger;

public partial class Index : ComponentBase
{
    private Table<RequestLogToListDto>? _table;
    private IEnumerable<RequestLogToListDto> _requestLogs = new List<RequestLogToListDto>();
    private int _totalCount;
    private int _pageSize = 10;
    private int _pageIndex = 1;

    private bool _responseModalVisible;
    private ResponseLogDto? _responseLog;
    private string _requestBody = string.Empty;
    private string _responseBody = string.Empty;
    private IEnumerable<CodeLogToListDto> _codeLogs = [];

    private string _searchQuery = string.Empty;
    private RequestLogSortType? _sortType;
    private SortOrder? _sortOrder;

    private TotalCountsResponseDto _totalCounts;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
        await LoadStatistics();
    }

    private async Task LoadData()
    {
        try
        {
            var dto = new GetRequestLogsDto
            (
                PageSize: _pageSize,
                PageIndex: _pageIndex,
                Query: _searchQuery,
                SortType: _sortType,
                SortOrder: _sortOrder
            );

            var result = await LogService.GetRequestLogs(dto);
            _requestLogs = result.Datas;
            _totalCount = result.TotalCount;
        }
        catch (System.Exception ex)
        {
            await MessageService.Error("Failed to load request logs");
        }
    }

    private async Task LoadStatistics()
    {
        try
        {
            _totalCounts = await StatisticalService.GetTotalCounts();
        }
        catch (System.Exception ex)
        {
            await MessageService.Error("Failed to load statistics");
        }
    }

    private async Task OnTableChange(QueryModel<RequestLogToListDto> queryModel)
    {
        _pageSize = queryModel.PageSize;
        _pageIndex = queryModel.PageIndex;

        if (queryModel.SortModel != null && queryModel.SortModel.Count > 0)
        {
            var sort = queryModel.SortModel.First();
            _sortType = sort.FieldName switch
            {
                nameof(RequestLogToListDto.TraceIdentifier) => RequestLogSortType.TraceIdentifier,
                nameof(RequestLogToListDto.Method) => RequestLogSortType.Method,
                nameof(RequestLogToListDto.Path) => RequestLogSortType.Path,
                nameof(RequestLogToListDto.RemoteIp) => RequestLogSortType.RemoteIp,
                nameof(RequestLogToListDto.CreatedAt) => RequestLogSortType.CreatedAt,
                _ => null
            };
            _sortOrder = sort.Sort == SortOrder.Ascending.ToString()
                ? SortOrder.Ascending
                : SortOrder.Descending;
        }
        else
        {
            _sortType = null;
            _sortOrder = null;
        }

        await LoadData();
    }

    private async Task OnSearch(string query)
    {
        _searchQuery = string.IsNullOrWhiteSpace(query) ? string.Empty : query;
        _pageIndex = 1;
        await LoadData();
    }

    private async Task ShowResponse(RequestLogToListDto request)
    {
        try
        {
            _responseLog = await LogService.GetResponseLog(request.HttpLogId);
            _requestBody = _responseLog.RequestBody;
            _responseBody = _responseLog.ResponseBody;

            // Fetch code logs for this request using the new endpoint
            _codeLogs = await LogService.GetCodeLogs(request.TraceIdentifier);

            _responseModalVisible = true;
        }
        catch (System.Exception ex)
        {
            await MessageService.Error("Failed to load response");
        }
    }

    private void CloseResponseModal()
    {
        _responseModalVisible = false;
        _responseLog = null;
    }
}