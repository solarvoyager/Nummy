using Microsoft.AspNetCore.Components;
using NummyShared.Dtos.Domain;
using NummyUi.Services;

namespace NummyUi.Components;

public partial class DsnPanel
{
    [Inject] private IHelperService HelperService { get; set; }
    private DsnResponseDto? dsn;
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _loading = true;
        StateHasChanged();
        
        dsn = await HelperService.GetDsn();
        
        _loading = false;
        StateHasChanged();
    }
}