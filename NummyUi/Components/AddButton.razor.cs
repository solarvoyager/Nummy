using Microsoft.AspNetCore.Components;

namespace NummyUi.Components;

public partial class AddButton : ComponentBase
{
    [Parameter] public EventCallback OnClick { get; set; }
    [Parameter] public string Title { get; set; }
    [Parameter] public string Description { get; set; }
}