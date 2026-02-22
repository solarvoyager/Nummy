using AntDesign;
using Microsoft.AspNetCore.Components;
using NummyShared.DTOs;
using NummyUi.Models.Application;
using NummyUi.Services.Abstract;

namespace NummyUi.Pages.Application;

public partial class Index
{
    [Inject] private IApplicationService ApplicationService { get; set; } = null!;
    [Inject] private IMessageService MessageService { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    private readonly ListGridType _grid = new()
    {
        Gutter = 16,
        Column = 4
    };

    private IEnumerable<ApplicationToListDto> _applications = [];
    private bool _applicationsLoading = true;

    #region ApplicationAddModel

    private IEnumerable<ApplicationStackToListDto> _stackTypes = [];
    private bool _stackTypesLoading = false;

    private bool _isAddOrEditModalVisible = false;
    private bool _isEdit = false;
    private ApplicationAddModel _applicationAddModel = new();
    private Guid? _editingId;

    private Form<ApplicationAddModel> _addApplicationForm = null!;

    #endregion


    protected override async Task OnInitializedAsync()
    {
        await Task.WhenAll(LoadApplications(), LoadStackTypes());
    }

    private async Task LoadApplications()
    {
        _applicationsLoading = true;
        _applications = await ApplicationService.Get();
        _applicationsLoading = false;
    }

    private async Task LoadStackTypes()
    {
        _stackTypesLoading = true;
        _stackTypes = await ApplicationService.GetStackType();
        _stackTypesLoading = false;
    }

    private void ShowModal(bool isEdit = false, ApplicationToListDto? application = null)
    {
        _isEdit = isEdit;
        if (isEdit && application != null)
        {
            _applicationAddModel = new ApplicationAddModel
            {
                Name = application.Name,
                Description = application.Description,
                StackTypeId = application.Stack.Id
            };
            _editingId = application.Id;
        }
        else
        {
            _applicationAddModel = new ApplicationAddModel();
            _editingId = null;
        }

        _isAddOrEditModalVisible = true;
    }

    private async Task HandleOk()
    {
        try
        {
            if (_addApplicationForm.Validate())
            {
                if (_isEdit && _editingId.HasValue)
                    await OnUpdate();
                else
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
        await ApplicationService.Add(
            _applicationAddModel.Name,
            _applicationAddModel.Description,
            _applicationAddModel.HealthCheckerUrl,
            _applicationAddModel.StackTypeId
        );

        await LoadApplications();
        _isAddOrEditModalVisible = false;

        await MessageService.Success("Application added successfully");
    }

    private async Task OnUpdate()
    {
        await ApplicationService.Update(
            _editingId!.Value,
            _applicationAddModel.Name,
            _applicationAddModel.Description,
            _applicationAddModel.HealthCheckerUrl,
            _applicationAddModel.StackTypeId
        );

        await LoadApplications();
        _isAddOrEditModalVisible = false;

        await MessageService.Success("Application updated successfully");
    }

    private void HandleCancel()
    {
        _isAddOrEditModalVisible = false;
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

    private void NavigateToConfiguration(ApplicationToListDto application)
    {
        NavigationManager.NavigateTo($"/application/configuration/{application.Id}");
    }
}
