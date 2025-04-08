using Microsoft.AspNetCore.Components;
using NummyShared.Dtos.Domain;
using NummyUi.Services;

namespace NummyUi.Pages.Dashboard.Analysis;

public partial class Index
{
    [Inject] private IStatisticalService StatisticalService { get; set; }
    
    private bool _loading;
    private TotalCountsResponseDto? _totalCounts;

    protected override async Task OnInitializedAsync()
    {
        await LoadDataAsync();
    }
    
    private async Task LoadDataAsync()
    {
        _loading = true;
        StateHasChanged();

        _totalCounts = await StatisticalService.GetTotalCounts();

        _loading = false;
        StateHasChanged();
    }
}