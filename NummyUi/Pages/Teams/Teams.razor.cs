using AntDesign;
using Microsoft.AspNetCore.Components;
using NummyShared.Dtos;
using NummyShared.DTOs;
using NummyUi.Services;

namespace NummyUi.Pages.Teams
{
    public partial class Teams
    {
        [Inject] private ITeamService TeamService { get; set; } = null!;
        [Inject] private IMessageService MessageService { get; set; } = null!;

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

        private async Task HandleEdit(TeamToListDto team)
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

        private async Task HandleDelete(TeamToListDto team)
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

        private async Task HandleShare(TeamToListDto team)
        {
            try
            {
                // TODO: Implement share functionality
                await MessageService.Info("Share functionality coming soon!");
            }
            catch (System.Exception ex)
            {
                await MessageService.Error("Failed to share team. Please try again later.");
            }
        }
    }
}