using NummyApi.Entitites.Generic;
using NummyShared.DTOs.Enums;

namespace NummyApi.Entitites;

public class ApplicationStack : Auditable
{
    public required ApplicationStackType Type { get; set; }
    public required string IconSvg { get; set; }
    public required string Title { get; set; }
}