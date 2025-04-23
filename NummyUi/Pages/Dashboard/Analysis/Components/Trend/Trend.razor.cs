using Microsoft.AspNetCore.Components;

namespace NummyUi.Pages.Dashboard.Analysis.Components.Trend
{
    public partial class Trend
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Flag { get; set; }
    }
}