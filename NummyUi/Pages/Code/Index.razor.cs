using AntDesign;
using Microsoft.AspNetCore.Components;
using NummyShared.Dtos;
using NummyShared.Dtos.Domain;
using NummyShared.Dtos.Enums;
using NummyUi.Services;

namespace NummyUi.Pages.Code;

public partial class Index
{
    [Inject] private ILogService LogService { get; set; }
    [Inject] private ModalService ModalService { get; set; }
    [Inject] private IMessageService MessageServices { get; set; }

    private Table<CodeLogToListDto>? _table;

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
        if (string.IsNullOrWhiteSpace(_query)) _query = string.Empty;

        await LoadDataAsync();
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
            /*// Open the Icon component directly without wrapping it in another component
            builder.OpenComponent<Icon>(1);
            builder.AddAttribute(2, "Type", "info-circle"); // Set the icon type, e.g., "info-circle"
            // Add any other attributes to the Icon component if needed
            builder.CloseComponent(); // Close the Icon component*/

            //builder.AddContent(1, "<p><strong>Key:</strong> Value</p>");
            //builder.AddContent(2, "<p><strong>Another Key:</strong> Another Value</p>");

            var typeColor = currentItem.LogLevel switch
            {
                CodeLogLevel.Trace => "#e6fffb",
                CodeLogLevel.Debug => "#e6f7ff",
                CodeLogLevel.Info => "#f6ffed",
                CodeLogLevel.Warning => "#fffbe6",
                CodeLogLevel.Error => "#fff2e8",
                CodeLogLevel.Fatal => "#fff0f6",
                _ => null
            };

            var typeBorderColor = currentItem.LogLevel switch
            {
                CodeLogLevel.Trace => "#1e9c9d",
                CodeLogLevel.Debug => "#627bdc",
                CodeLogLevel.Info => "#50a581",
                CodeLogLevel.Warning => "#db9648",
                CodeLogLevel.Error => "#df7526",
                CodeLogLevel.Fatal => "#c93398",
                _ => null
            };

            var keyValuePairs = $"""
                                    <div style="margin-bottom: 12px"><strong>{nameof(currentItem.TraceIdentifier)}</strong><br/><p style="color: LightSlateGray">{currentItem.TraceIdentifier}</p></div>
                                    <div style="margin-bottom: 12px"><strong>{nameof(currentItem.LogLevel)}</strong><br/><div style="background-color: {typeColor}; color: {typeBorderColor}; border-color: {typeBorderColor}; border-width: 1px; margin-top: 6px; padding: 4px 10px; font-size: 14px; border-radius: 3px; display: inline-block;">{currentItem.LogLevel}</div></div>
                                    <div style="margin-bottom: 12px"><strong>{nameof(currentItem.CreatedAt)}</strong><br/><p style="color: LightSlateGray">{currentItem.CreatedAt}</p></div>
                                    <div style="margin-bottom: 12px"><strong>{nameof(currentItem.ExceptionType)}</strong><br/><p style="color: LightSlateGray">{currentItem.ExceptionType}</p></div>
                                    <div style="margin-bottom: 12px"><strong>{nameof(currentItem.Title)}</strong><br/><p style="color: LightSlateGray">{currentItem.Title}</p></div>
                                    <div style="margin-bottom: 12px"><strong>{nameof(currentItem.Description)}</strong><br/><p style="color: LightSlateGray">{currentItem.Description}</p></div>
                                    <div style="margin-bottom: 12px"><strong>{nameof(currentItem.InnerException)}</strong><br/><p style="color: LightSlateGray">{currentItem.InnerException}</p></div>
                                    <div style="margin-bottom: 12px"><strong>{nameof(currentItem.StackTrace)}</strong><br/><p style="color: LightSlateGray">{currentItem.StackTrace}</p></div>
                                 """;

            builder.AddContent(1, new MarkupString(keyValuePairs));
        };

        ModalService.CreateModalAsync(new ModalOptions()
        {
            Title = $"Id: {currentItem.Id}",
            //Icon =  ,
            Content = content,
            Resizable = true,
            Maximizable = true,
            Width = 800
        });
    }

    private void ShowDeleteModal(bool isAll, Guid? id)
    {
        async Task OnOk(ModalClosingEventArgs _)
        {
            var ids = isAll
                ? _items.Select(i => i.Id)
                : _items.Where(i => i.Id == id).Select(i => i.Id);

            await LogService.DeleteCodeLogs(new DeleteCodeLogsDto(ids.ToList()));

            OnUnselectAll();

            MessageServices.Success("Deleted successfully");
            await LoadDataAsync();
        }

        ModalService.Confirm(new ConfirmOptions
        {
            Title = "Are you sure?",
            //Icon =  IconType.Outline.Search,
            OkText = "Yes",
            CancelText = "No",
            Content = isAll ? "This operation will delete all items" : "This operation will delete this item",
            OnOk = OnOk,
            OkType = "danger",
        });
    }
}