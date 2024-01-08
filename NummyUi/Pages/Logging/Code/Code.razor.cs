using System.ComponentModel;
using System.Web;
using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using NummyUi.Dtos;
using NummyUi.Services;

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
        _total = 50;
        await LoadDataAsync();
    }

    private void PageIndexChanged(PaginationEventArgs args)
    {
        _pageSize = args.PageSize;
        _pageIndex = args.Page;
        
        LoadDataAsync().Wait();
    }

    private async Task LoadDataAsync()
    {
        _loading = true;
        StateHasChanged();
        
        _codeLogs = (await LogService.GetCodeLogs(_pageIndex, _pageSize)).ToArray();
        
        _loading = false;
        StateHasChanged();
    }
}