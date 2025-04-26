using System.ComponentModel.DataAnnotations;
using AntDesign;
using Microsoft.AspNetCore.Components;
using NummyUi.Models;
using NummyUi.Models.Account;
using NummyUi.Services;

namespace NummyUi.Pages.User.Register
{
    public partial class Index
    {
        private readonly RegisterModel _model = new();
        
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IMessageService Message { get; set; }
        [Inject] public IUserService UserService { get; set; }
        
        private bool _isLoading = false;

        public async Task OnRegister()
        {
            _isLoading = true;
            
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
                Message.Error($"Sign Up failed: {registerResult.Message}");
            }

            _isLoading = false;
        }
    }
}