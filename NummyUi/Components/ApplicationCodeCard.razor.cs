using Microsoft.AspNetCore.Components;
using NummyShared.DTOs.Domain;
using NummyUi.Services;

namespace NummyUi.Components;

public partial class ApplicationCodeCard
{
    [Parameter] public string ApplicationCode { get; set; }
}