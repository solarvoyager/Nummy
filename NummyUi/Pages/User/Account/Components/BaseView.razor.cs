using Microsoft.AspNetCore.Components;
using NummyShared.Dtos;
using NummyUi.Models;
using NummyUi.Services;
using NummyUi.Session;

namespace NummyUi.Pages.User.Account.Components
{
    public partial class BaseView
    {
        private BaseViewModel _model;

        [Inject] protected IUserSession UserSession { get; set; }

        private void HandleFinish()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var user = UserSession.GetUser();

            _model = new BaseViewModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone
                //Avatar = user.Avatar
            };
        }
    }
}