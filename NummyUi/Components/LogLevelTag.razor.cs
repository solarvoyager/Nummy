using AntDesign;
using Microsoft.AspNetCore.Components;
using NummyShared.Dtos.Enums;

namespace NummyUi.Components;

public partial class LogLevelTag : ComponentBase
{
    [Parameter] public CodeLogLevel LogLevel { get; set; }
    
    private static string GetColorByLogLevel(CodeLogLevel level)
    {
        return level switch
        {
            CodeLogLevel.Fatal => PresetColor.Magenta.ToString(),
            CodeLogLevel.Error => PresetColor.Volcano.ToString(),
            CodeLogLevel.Warning => PresetColor.Gold.ToString(), CodeLogLevel.Debug => PresetColor.Blue.ToString(),
            CodeLogLevel.Trace => PresetColor.Cyan.ToString(), CodeLogLevel.Info => PresetColor.Green.ToString(),
            _ => "default"
        };
    }
}