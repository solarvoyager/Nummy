using Microsoft.AspNetCore.Components;
using NummyUi.Models;
using NummyUi.Services;

namespace NummyUi.Pages.User.Account.Components
{
    public partial class BaseView
    {
        private CurrentUser _currentUser = new()
        {
            Email = "antdesign@alipay.com",
            Name = "Serati Ma",
            Signature = "Be tolerant to diversity, tolerance is a virtue",
            Country = "China",
            Address = "",
            Avatar = "/avatar.png"
        };

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