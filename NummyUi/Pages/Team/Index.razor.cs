using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NummyShared.DTOs;
using NummyUi.Services;
using NummyUi.Utils;

namespace NummyUi.Pages.Team;

public partial class Index
{
    [Inject] private ITeamService TeamService { get; set; } = null!;
    [Inject] private IMessageService MessageService { get; set; } = null!;
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;
    
    private readonly ListGridType _listGridType = new ListGridType
    {
        Gutter = 24,
        Column = 4
    };

    private IEnumerable<TeamToListDto> _teams = new List<TeamToListDto>();
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
    {
        await LoadTeams();
    }

    private async Task LoadTeams()
    {
        _loading = true;
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
            _loading = false;
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
            await JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", NummyContants.TeamShareUrl + $"/{team.Id}");
            await MessageService.Info("Team sharing url copied to clipboard!");
        }
        catch (System.Exception ex)
        {
            await MessageService.Error("Failed to share team. Please try again later.");
        }
    }
}