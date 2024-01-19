using Microsoft.AspNetCore.Components;
using NummyUi.Models;
using NummyUi.Services;
using System.Threading.Tasks;

namespace NummyUi.Pages.Account.Profile
{
    public partial class BaseView
    {
        private CurrentUser _currentUser = new CurrentUser();

        [Inject] protected IUserService UserService { get; set; }

        private void HandleFinish()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _currentUser = await UserService.GetCurrentUserAsync();
        }
    }
}