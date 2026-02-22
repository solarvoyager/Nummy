using AntDesign;
using Microsoft.AspNetCore.Components;
using NummyUi.Models.Account;
using NummyUi.Services.Abstract;
using NummyUi.Session;

namespace NummyUi.Pages.User.Login
{
    public partial class Index
    {
        private readonly LoginModel _model = new();

        [Inject] public NavigationManager NavigationManager { get; set; } = null!;
        [Inject] public IMessageService Message { get; set; } = null!;
        [Inject] public IUserService UserService { get; set; } = null!;
        [Inject] public IUserSession UserSession { get; set; } = null!;

        private bool _isLoading = false;

        public async Task OnLogin()
        {
            _isLoading = true;

            try
            {
                var loginResult = await UserService.Login(_model.Email, _model.Password);

                if (loginResult.Success)
                {
                    var user = await UserService.Get(loginResult.UserId!.Value);
                    UserSession.SetUser(user);
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    await Message.Error($"Sign In failed: {loginResult.Message}");
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
