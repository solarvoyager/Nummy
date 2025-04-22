using Microsoft.AspNetCore.Components;
using NummyShared.Dtos;
using NummyUi.Models;
using NummyUi.Services;

namespace NummyUi.Pages.User.Account.Components
{
    public partial class BaseView
    {
        private UserToListDto _user;

        [Inject] protected IUserService UserService { get; set; }

        private void HandleFinish()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            //_currentUser = await UserService.GetCurrentUserAsync();
        }
    }
}