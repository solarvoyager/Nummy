using Microsoft.AspNetCore.Components;
using NummyUi.Services;

namespace NummyUi.Pages.Database.Dashboard;

public partial class Index
{
    private IEnumerable<string> _pendingMigrations = new List<string>();
    private bool? _migrationResult = null;
    [Inject] public IDatabaseService DatabaseService { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _pendingMigrations = await DatabaseService.GetPendingMigrations();
    }

    private async void Migrate()
    {
        _migrationResult = await DatabaseService.Migrate();
        await InvokeAsync(() => StateHasChanged());
    }

}