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

    ITable? _table;

    private IEnumerable<CodeLogToListDto> _items = new List<CodeLogToListDto>();
    private IEnumerable<CodeLogToListDto> _selectedItems = new List<CodeLogToListDto>();

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

    private void OnSelectionChanged(IEnumerable<CodeLogToListDto> selectedItems)
    {
        _selectedItems = selectedItems;
    }

    // todo fix this
    /*private void OnSelectionRemoved(Guid id)
    {
        var itemToRemove = _selectedItems.First(i => i.Id == id);
        _selectedItems.Remove(itemToRemove);

        //_table!.SetSelection(_selectedItems.Select(i=>i.Id.ToString()).ToList());
        _table!.ReloadData();
    }*/

    private void OnUnselectAll()
    {
        _table!.UnselectAll();
    }

    private void OnDelete(Guid id)
    {
        ShowDeleteModal(false, id);
    }

    private void OnDeleteAll()
    {
        ShowDeleteModal(true, null);
    }

    private async Task LoadDataAsync()
    {
        _loading = true;
        StateHasChanged();

        List<CodeLogLevel> pickedLevels = [];
        _logLevelFilters.Where(l => l.Value).ForEach(l => pickedLevels.Add(l.Key));

        var request = new GetCodeLogsDto(
            _pageSize,
            _pageIndex,
            _query,
            _sortType,
            _sortOrder,
            pickedLevels);

        var result = await LogService.GetCodeLogs(request);

        _items = result.Datas;
        _total = result.TotalCount;

        _loading = false;
        StateHasChanged();
    }

    private void ShowViewModal(Guid id)
    {
        var currentItem = _items.First(i => i.Id == id);

        RenderFragment content = builder =>
        {
            builder.OpenComponent<Text>(1);
            //builder.AddAttribute(2, "Style", "font-size: 29px");
            builder.AddContent(1, "<p>Text</>");
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

    private void ShowDeleteModal(bool isAll, Guid? id)
    {
        Func<ModalClosingEventArgs, Task> onOk = async (e) =>
        {
            var ids = isAll
                ? _selectedItems.Select(i => i.Id)
                : _selectedItems.Where(i => i.Id == id).Select(i => i.Id);

            await LogService.DeleteCodeLogs(new DeleteCodeLogsDto(ids.ToList()));

            await LoadDataAsync();
        };

        Func<ModalClosingEventArgs, Task> onCancel = (e) =>
        {
            Console.WriteLine("Cancel");
            return Task.CompletedTask;
        };


        ModalService.Confirm(new ConfirmOptions()
        {
            Title = "Are you sure?",
            //Icon =  IconType.Outline.Search,
            Content = isAll ? "This operation will delete all items" : "This operation will delete this item",
            OnOk = onOk,
            OnCancel = onCancel,
            OkType = "danger",
        });
    }
}