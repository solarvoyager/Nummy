using AntDesign;
using Microsoft.AspNetCore.Components;
using NummyUi.Models.Account;
using NummyUi.Services.Abstract;

namespace NummyUi.Pages.User.Register
{
    public partial class Index
    {
        private readonly RegisterModel _model = new();

        [Inject] public NavigationManager NavigationManager { get; set; } = null!;
        [Inject] public IMessageService Message { get; set; } = null!;
        [Inject] public IUserService UserService { get; set; } = null!;

        private bool _isLoading = false;

        public async Task OnRegister()
        {
            _isLoading = true;

            try
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
                    await Message.Error($"Sign Up failed: {registerResult.Message}");
                }
            }
            catch (System.Exception ex)
            {
                await Message.Error($"Unexpected error: {ex.Message}");
            }
            finally
            {
                _isLoading = false;
            }
        }
    }
}
