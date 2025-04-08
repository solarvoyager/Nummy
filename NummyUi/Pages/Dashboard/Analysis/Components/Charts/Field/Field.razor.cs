using Microsoft.AspNetCore.Components;

namespace NummyUi.Pages.Dashboard.Analysis.Components.Charts.Field
{
    public partial class Field
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string Value { get; set; }
        
        [Parameter]
        public string? Description { get; set; }
    }
}