using System.ComponentModel.DataAnnotations;
using AntDesign;
using Microsoft.AspNetCore.Components;
using NummyUi.Models;
using NummyUi.Services;

namespace NummyUi.Pages.User.Register
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //[Required]
        public string? Phone { get; set; }

        //[Required]
        public string Captcha { get; set; }
    }

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