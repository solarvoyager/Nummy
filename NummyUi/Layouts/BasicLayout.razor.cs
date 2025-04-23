using AntDesign;
using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components;
using NummyUi.Utils;

namespace NummyUi.Layouts;

public partial class BasicLayout
{
    private bool collapsed;
    private string _selectedKey = "1";
    
    [Inject] public NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        SetSelectedKey();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        SetSelectedKey();
    }

    private void SetSelectedKey()
    {
        var currentPath = NavigationManager.Uri.Replace(NavigationManager.BaseUri, "/");
        var selectedMenu = NummyContants.MenuDataItems.FirstOrDefault(m => m.Path == currentPath);
        if (selectedMenu != null)
        {
            _selectedKey = selectedMenu.Key;
        }
    }

    private void OnMenuItemSelected(MenuItem menuItem)
    {
        var selectedMenu = NummyContants.MenuDataItems.FirstOrDefault(m => m.Key == menuItem.Key);
        
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
            Href = NummyContants.GitHubUrl,
            BlankTarget = true,
        }
    ];
}