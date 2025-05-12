using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NummyShared.DTOs;
using NummyUi.Models.Team;
using NummyUi.Services;
using NummyUi.Utils;

namespace NummyUi.Pages.Team;

public partial class Index
{
    [Inject] private ITeamService TeamService { get; set; } = null!;
    [Inject] private IUserService UserService { get; set; } = null!;
    [Inject] private IApplicationService ApplicationService { get; set; } = null!;
    [Inject] private IMessageService MessageService { get; set; } = null!;
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    private readonly ListGridType _listGridType = new()
    {
        Gutter = 24,
        Column = 4
    };

    private IEnumerable<TeamToListDto> _teams = new List<TeamToListDto>();
    private bool _teamsLoading = true;

    #region AddTeamModal

    private IEnumerable<UserToListDto> _allUsers = new List<UserToListDto>();
    private bool _allUsersLoading = true;

    private IEnumerable<ApplicationToListDto> _allApplications = new List<ApplicationToListDto>();
    private bool _allApplicationsLoading = true;

    private bool _isAddTeamModalVisible;
    
    private readonly TeamAddModel _teamAddModel = new();
    private Form<TeamAddModel> _addTeamForm = null!;

    #endregion

    protected override async Task OnInitializedAsync()
    {
        await LoadTeams();
        await LoadUsers();
        await LoadApplications();
    }

    private async Task LoadTeams()
    {
        _teamsLoading = true;
        try
        {
            _teams = await TeamService.Get();
        }
        catch (System.Exception ex)
        {
            await MessageService.Error("Failed to load teams. Please try again later.");
        }
        finally
        {
            _teamsLoading = false;
            StateHasChanged();
        }
    }

    private async Task LoadUsers()
    {
        _allUsersLoading = true;
        try
        {
            _allUsers = await UserService.Get();
        }
        catch (System.Exception ex)
        {
            await MessageService.Error("Failed to load users. Please try again later.");
        }
        finally
        {
            _allUsersLoading = false;
            StateHasChanged();
        }
    }
    
    private async Task LoadApplications()
    {
        _allApplicationsLoading = true;
        try
        {
            _allApplications = await ApplicationService.Get();
        }
        catch (System.Exception ex)
        {
            await MessageService.Error("Failed to load applications. Please try again later.");
        }
        finally
        {
            _allApplicationsLoading = false;
            StateHasChanged();
        }
    }

    private async Task OnEdit(TeamToListDto team)
    {
        try
        {
            // TODO: Implement edit functionality
            await MessageService.Info("Edit functionality coming soon!");
        }
        catch (System.Exception ex)
        {
            await MessageService.Error("Failed to edit team. Please try again later.");
        }
    }

    private async Task OnDelete(TeamToListDto team)
    {
        try
        {
            await TeamService.Delete(team.Id);
            await LoadTeams();

            await MessageService.Success("Team deleted successfully!");
        }
        catch (System.Exception ex)
        {
            await MessageService.Error("Failed to delete team. Please try again later.");
        }
    }

    private async Task OnShare(TeamToListDto team)
    {
        try
        {
            await JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText",
                NummyContants.TeamShareUrl + $"/{team.Id}");
            await MessageService.Info("Team sharing url copied to clipboard!");
        }
        catch (System.Exception ex)
        {
            await MessageService.Error("Failed to share team. Please try again later.");
        }
    }

    private async Task HandleAddTeam()
    {
        try
        {
            if (_addTeamForm.Validate() && _teamAddModel.SelectedUserIds.Any())
            {
                await TeamService.Add(_teamAddModel.Name, _teamAddModel.Description, _teamAddModel.SelectedUserIds, _teamAddModel.SelectedApplicationIds);
                await LoadTeams();

                _teamAddModel.Name = string.Empty;
                _teamAddModel.Description = string.Empty;
                _teamAddModel.SelectedUserIds = [];
                _teamAddModel.SelectedApplicationIds = [];

                _isAddTeamModalVisible = false;

                await MessageService.Success("Team created successfully!");
            }
        }
        catch (System.Exception ex)
        {
            await MessageService.Error("Failed to create team. Please try again later.");
        }
    }

    private void RemoveUser(Guid userId)
    {
        _teamAddModel.SelectedUserIds = _allUsers
            .Where(u => u.Id != userId)
            .Select(u => u.Id)
            .ToList();
    }
    
    private void RemoveApplication(Guid applicationId)
    {
        _teamAddModel.SelectedApplicationIds = _allApplications
            .Where(a => a.Id != applicationId)
            .Select(a => a.Id)
            .ToList();
    }
}