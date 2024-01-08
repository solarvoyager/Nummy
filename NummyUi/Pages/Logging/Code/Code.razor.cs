using System.ComponentModel;
using System.Web;
using AntDesign;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using NummyUi.Services;
using NummyUi.Dtos;

namespace NummyUi.Pages.Logging.Code;

public partial class Code
{
    [Inject] private ILogService LogService { get; set; }
    
    private CodeLogToListDto[]? _codeLogs;
    
    private int _pageIndex = 1;
    private int _pageSize = 5;
    private int _total = 0;
    private bool _loading;

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

    private async Task LoadDataAsync()
    {
        _loading = true;
        StateHasChanged();

        var result = await LogService.GetCodeLogs(_pageIndex, _pageSize);

        _codeLogs = result.Datas.ToArray();
        _total = result.TotalCount;
        
        _loading = false;
        StateHasChanged();
    }
}