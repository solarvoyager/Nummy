using AntDesign;
using Microsoft.AspNetCore.Components;
using NummyUi.Services;

namespace NummyUi.Components.MigrationPanel;

public partial class MigrationPanel
{
    [Inject] private IDatabaseService DatabaseService { get; set; }
    private IEnumerable<string> _pendingMigrations = new List<string>();
    private bool? _migrationResult;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _pendingMigrations = await DatabaseService.GetPendingMigrations();
    }

    private async void Migrate()
    {
        _migrationResult = await DatabaseService.Migrate();

        if (_migrationResult.Value)
            _pendingMigrations = [];
        
        await InvokeAsync(StateHasChanged);
    }
}