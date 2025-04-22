using System.ComponentModel.DataAnnotations;
using AntDesign;
using Microsoft.AspNetCore.Components;
using NummyUi.Models;
using NummyUi.Services;

namespace NummyUi.Pages.User.Register
{
    public partial class Index
    {
        private readonly RegisterModel _model = new();
        
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public MessageService Message { get; set; }
        [Inject] public IUserService UserService { get; set; }

        public async Task Register()
        {
            var registerResult = await UserService.Register(
                _model.Name,
                _model.Surname,
                _model.Email,
                string.IsNullOrWhiteSpace(_model.Phone) ? null : _model.Phone,
                _model.Password
            );
            
            if (registerResult.Success)
            {
                NavigationManager.NavigateTo("/user/login");
            }
            else
            {
                await Message.Error($"Register failed: {registerResult.Message}");
            }
        }
    }
}