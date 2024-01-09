using System.ComponentModel;
using System.Web;
using AntDesign;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using NummyApi.Enums;
using NummyUi.Services;
using NummyUi.Dtos;
using NummyUi.Dtos.Domain;

namespace NummyUi.Pages.Logging.Code;

public partial class Code
{
    [Inject] private ILogService LogService { get; set; }

    private CodeLogToListDto[]? _codeLogs;

    private int _pageIndex = 1;
    private int _pageSize = 5;
    private int _total = 0;
    private bool _loading;

    private Dictionary<CodeLogLevel, bool> _logLevelFilters = new()
    {
        { CodeLogLevel.Trace, true },
        { CodeLogLevel.Debug, true },
        { CodeLogLevel.Info, true },
        { CodeLogLevel.Warning, true },
        { CodeLogLevel.Error, true },
        { CodeLogLevel.Fatal, true }
    };

    protected override async Task OnInitializedAsync()
    {
        await LoadDataAsync();
    }

    private async Task PageIndexChanged(PaginationEventArgs args)
    {
        _pageSize = args.PageSize;
        _pageIndex = args.Page;

        await LoadDataAsync();
    }

    private async Task CheckFilterChanged(bool isChecked, CodeLogLevel level)
    {
        _logLevelFilters[level] = isChecked;

        await LoadDataAsync();
    }
    private async Task LoadDataAsync()
    {
        _loading = true;
        StateHasChanged();

        ICollection<CodeLogLevel> pickedLevels = new List<CodeLogLevel>();
        _logLevelFilters.Where(l=>l.Value).ForEach(l=>pickedLevels.Add(l.Key));

        var result = await LogService.GetCodeLogs(new GetCodeLogsRequestDto(_pageSize, _pageIndex, pickedLevels));

        _codeLogs = result.Datas.ToArray();
        _total = result.TotalCount;

        _loading = false;
        StateHasChanged();
    }
}