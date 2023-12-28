using Microsoft.AspNetCore.Components;
using NummyUi.Models;
using System.Collections.Generic;

namespace NummyUi.Pages.Account.Center
{
    public partial class Articles
    {
        [Parameter] public IList<ListItemDataType> List { get; set; }
    }
}