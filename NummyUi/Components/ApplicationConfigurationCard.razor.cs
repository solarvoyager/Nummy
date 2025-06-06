using Microsoft.AspNetCore.Components;
using NummyShared.DTOs.Domain;
using NummyUi.Services;

namespace NummyUi.Components;

public partial class ApplicationConfigurationCard
{
    [Inject] private IHelperService HelperService { get; set; } = null!;

    [Parameter] public string ApplicationId { get; set; } = null!;

    private bool _isLoading = true;
    private string _serviceUrl = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await LoadServiceUrl();
        await base.OnInitializedAsync();
    }

    private async Task LoadServiceUrl()
    {
        _isLoading = true;

        var serviceUrl = await HelperService.GetServiceUrl();
        _serviceUrl = serviceUrl.ServiceUrl;

        _isLoading = false;
    }
}