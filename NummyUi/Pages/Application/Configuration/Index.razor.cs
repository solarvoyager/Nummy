using Microsoft.AspNetCore.Components;
using NummyShared.DTOs;
using NummyUi.Services;

namespace NummyUi.Pages.Application.Configuration;

public partial class Index : ComponentBase
{
    [Inject] public IApplicationService ApplicationService { get; set; } = null!;
    
    [Parameter] public Guid ApplicationId { get; set; }
    
    private ApplicationToListDto? _application;
    
    protected override async Task OnInitializedAsync()
    {
        _application = await ApplicationService.Get(ApplicationId);
    }
}