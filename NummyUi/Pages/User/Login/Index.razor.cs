using System.ComponentModel.DataAnnotations;
using AntDesign;
using Microsoft.AspNetCore.Components;
using NummyUi.Models;
using NummyUi.Models.Account;
using NummyUi.Services;
using NummyUi.Session;

namespace NummyUi.Pages.User.Login
{
    public partial class Index
    {
        private readonly LoginModel _model = new();

        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IMessageService Message { get; set; }
        [Inject] public IUserService UserService { get; set; }
        [Inject] public IUserSession UserSession { get; set; }

        private bool _isLoading = false;

        public async Task OnLogin()
        {
            _isLoading = true;
            
            var loginResult = await UserService.Login(_model.Email, _model.Password);
            
            if (loginResult.Success)
            {
                var user = await UserService.Get(loginResult.UserId!.Value);
                UserSession.SetUser(user);
                
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Message.Error($"Sign In failed: {loginResult.Message}");
            }
            
            _isLoading = false;
        }

        /*public async Task GetCaptcha()
        {
            var captcha = await AccountService.GetCaptchaAsync(_model.Mobile);
            await Message.Success($"Verification code validated successfully! The verification code is: {captcha}");
        }*/
    }
}