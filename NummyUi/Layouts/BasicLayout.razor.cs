using AntDesign;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using NummyUi.Session;
using NummyUi.Utils;

namespace NummyUi.Layouts;

public partial class BasicLayout
{
    private bool collapsed;
    private string _selectedKey = "1";

    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public IUserSession UserSession { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        SetSelectedKey();
        RedirectIfNotAuthenticated();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        SetSelectedKey();
        RedirectIfNotAuthenticated();
    }

    private void RedirectIfNotAuthenticated()
    {
        var currentPath = NavigationManager.Uri.Replace(NavigationManager.BaseUri, "/");
        var isAuthPage = currentPath.Contains("/user/login") || currentPath.Contains("/user/register");

        if (!isAuthPage && !UserSession.IsLoggedIn())
            NavigationManager.NavigateTo("/user/login", forceLoad: false);
    }

    private void SetSelectedKey()
    {
        var currentPath = NavigationManager.Uri.Replace(NavigationManager.BaseUri, "/");
        var selectedMenu = NummyConstants.MenuDataItems.FirstOrDefault(m => m.Path == currentPath);
        if (selectedMenu != null)
            _selectedKey = selectedMenu.Key;
    }

    private void OnMenuItemSelected(MenuItem menuItem)
    {
        var selectedMenu = NummyConstants.MenuDataItems.FirstOrDefault(m => m.Key == menuItem.Key);

        if (selectedMenu != null)
        {
            NavigationManager.NavigateTo(selectedMenu.Path);
            _selectedKey = menuItem.Key;
        }
    }

    public LinkItem[] Links { get; set; } =
    [
        new()
        {
            Key = "github",
            Title = (RenderFragment)(builder =>
            {
                builder.OpenComponent<Icon>(0);
                builder.AddAttribute(1, "Type", "github");
                builder.CloseComponent();
            }),
            Href = NummyConstants.GitHubUrl,
            BlankTarget = true,
        }
    ];
}
