using System.ComponentModel;
using System.Web;
using AntDesign;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using NummyUi.Services;
using NummyUi.Dtos;
using NummyUi.Dtos.Domain;
using NummyUi.Dtos.Enums;

namespace NummyUi.Pages.Logging.Code;

public partial class Code
{
    [Inject] private ILogService LogService { get; set; }
    [Inject] private ModalService ModalService { get; set; }

    private CodeLogToListDto[]? _items;

    private int _pageIndex = 1;
    private int _pageSize = 10;
    private int _total = 0;
    private bool _loading;

    private string _query = string.Empty;
    private SortOrder _sortOrder = SortOrder.Ascending;
    private CodeLogSortType? _sortType;

    private readonly Dictionary<CodeLogLevel, bool> _logLevelFilters = new()
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

    private async Task OnPageIndexChanged(PaginationEventArgs args)
    {
        _pageSize = args.PageSize;
        _pageIndex = args.Page;

        await LoadDataAsync();
    }

    private async Task OnCheckFilterChanged(bool isChecked, CodeLogLevel level)
    {
        _logLevelFilters[level] = isChecked;

        await LoadDataAsync();
    }

    private async Task OnSearch()
    {
        if (!string.IsNullOrWhiteSpace(_query))
        {
            await LoadDataAsync();
        }
    }

    private async Task OnSortOrderChanged(SortOrder sortOrder)
    {
        _sortOrder = sortOrder;
        await LoadDataAsync();
    }

    private async Task OnSortTypeChanged(CodeLogSortType sortType)
    {
        _sortType = sortType;
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        _loading = true;
        StateHasChanged();

        List<CodeLogLevel> pickedLevels = [];
        _logLevelFilters.Where(l => l.Value).ForEach(l => pickedLevels.Add(l.Key));

        var request = new GetCodeLogsRequestDto(
            _pageSize,
            _pageIndex,
            _query,
            _sortType,
            _sortOrder,
            pickedLevels);

        var result = await LogService.GetCodeLogs(request);

        _items = result.Datas.ToArray();
        _total = result.TotalCount;

        _loading = false;
        StateHasChanged();
    }

    private void ShowViewModal(Guid id)
    {
        var currentItem = _items!.First(i => i.Id == id);
        
        RenderFragment content = builder =>
        {
            builder.OpenComponent<Text>(1);
            //builder.AddAttribute(2, "Style", "font-size: 29px");
            builder.AddContent(1,  "<p>Text</>");
            builder.CloseComponent();
        };
        
        ModalService.Info(new ConfirmOptions
        {
            Title = $"{currentItem.Title}",
            //Icon =  ,
            Content = content,
            OnCancel = (e) =>
            {
                Console.WriteLine("Ok");
                return Task.CompletedTask;
            }
        });
    }

    private void ShowDeleteModal()
    {
        Func<ModalClosingEventArgs, Task> onOk = (e) =>
        {
            Console.WriteLine("Ok");
            return Task.CompletedTask;
        };

        Func<ModalClosingEventArgs, Task> onCancel = (e) =>
        {
            Console.WriteLine("Cancel");
            return Task.CompletedTask;
        };


        ModalService.Confirm(new ConfirmOptions()
        {
            Title = "Are you sure delete this task?",
            //Icon =  ,
            Content = "Some descriptions",
            OnOk = onOk,
            OnCancel = onCancel,
            OkType = "danger",
        });
    }
}