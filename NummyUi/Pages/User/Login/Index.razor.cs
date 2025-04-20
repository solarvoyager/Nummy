using System.ComponentModel.DataAnnotations;
using AntDesign;
using Microsoft.AspNetCore.Components;
using NummyUi.Models;
using NummyUi.Services;

namespace NummyUi.Pages.User.Login
{
    public class LoginModel
    {
        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }

        public string Mobile { get; set; }

        public string Captcha { get; set; }

        public string LoginType { get; set; }

        public bool AutoLogin { get; set; }
    }
    
    public partial class Index
    {
        private readonly LoginModel _model = new();

        [Inject] public NavigationManager NavigationManager { get; set; }

        [Inject] public MessageService Message { get; set; }
        
        [Inject] public IUserService UserService { get; set; }

        public async Task Login()
        {
            var loginResult = await UserService.Login(_model.Email, _model.Password);
            
            if (loginResult.Success)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                await Message.Error($"Login failed: {loginResult.Message}");
            }
        }

        /*public async Task GetCaptcha()
        {
            var captcha = await AccountService.GetCaptchaAsync(_model.Mobile);
            await Message.Success($"Verification code validated successfully! The verification code is: {captcha}");
        }*/
    }
}