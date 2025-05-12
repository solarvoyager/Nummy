using AntDesign;
using Microsoft.AspNetCore.Components;
using NummyShared.DTOs;
using NummyUi.Models.Application;
using NummyUi.Services;

namespace NummyUi.Pages.Application;

public partial class Index
{
    [Inject] private IApplicationService ApplicationService { get; set; } = null!;
    [Inject] private IMessageService MessageService { get; set; } = null!;
    
    private readonly ListGridType _grid = new()
    {
        Gutter = 16,
        Column = 4
    };

    private IEnumerable<ApplicationToListDto> Applications { get; set; } = [];
    private bool _applicationsLoading = true;

    private bool _isTeamEditOrAddModalVisible;
    private bool IsEdit { get; set; }
    private ApplicationAddModel Model { get; set; } = new();
    private Guid? EditingId { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await LoadApplications();
    }

    private async Task LoadApplications()
    {
        _applicationsLoading = true;
        Applications = await ApplicationService.Get();
        _applicationsLoading = false;
    }

    private void ShowModal(bool isEdit = false, ApplicationToListDto? application = null)
    {
        IsEdit = isEdit;
        if (isEdit && application != null)
        {
            Model = new ApplicationAddModel
            {
                Name = application.Name,
                Description = application.Description
            };
            EditingId = application.Id;
        }
        else
        {
            Model = new ApplicationAddModel();
            EditingId = null;
        }

        _isTeamEditOrAddModalVisible = true;
    }

    private async Task HandleOk()
    {
        try
        {
            if (IsEdit && EditingId.HasValue)
            {
                await OnUpdate();
            }
            else
            {
                await OnAdd();
            }
        }
        catch (System.Exception ex)
        {
            await MessageService.Error($"Error: {ex.Message}");
        }
    }

    private async Task OnAdd()
    {
        await ApplicationService.Add(Model.Name, Model.Description);
        
        await LoadApplications();
        _isTeamEditOrAddModalVisible = false;
        
        await MessageService.Success("Application added successfully");
    }

    private async Task OnUpdate()
    {
        await ApplicationService.Update(EditingId.Value, Model.Name, Model.Description);
        
        await LoadApplications();
        _isTeamEditOrAddModalVisible = false;
        
        await MessageService.Success("Application updated successfully");
    }

    private void HandleCancel()
    {
        _isTeamEditOrAddModalVisible = false;
    }

    private async Task OnDelete(Guid id)
    {
        try
        {
            await ApplicationService.Delete(id);
            await LoadApplications();

            await MessageService.Success("Application deleted successfully");
        }
        catch (System.Exception ex)
        {
            await MessageService.Error($"Error: {ex.Message}");
        }
    }
}