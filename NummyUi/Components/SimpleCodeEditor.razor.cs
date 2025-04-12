using System.Text.Json;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace NummyUi.Components;

public partial class SimpleCodeEditor : ComponentBase
{
    private string _content = string.Empty;
    private bool _isCopied = false;
    private System.Timers.Timer _copyFeedbackTimer;
    private int LineCount => string.IsNullOrEmpty(Content) ? 1 : Content.Split('\n').Length;

    [Parameter]
    public string Content
    {
        get => _content;
        set
        {
            if (_content != value)
            {
                _content = value;
                ContentChanged.InvokeAsync(value);
            }
        }
    }

    [Parameter] public EventCallback<string> ContentChanged { get; set; }

    protected override void OnInitialized()
    {
        _copyFeedbackTimer = new System.Timers.Timer(2000);
        _copyFeedbackTimer.Elapsed += (sender, e) =>
        {
            _isCopied = false;
            InvokeAsync(StateHasChanged);
            _copyFeedbackTimer.Stop();
        };
    }

    private async Task CopyCode()
    {
        try
        {
            await JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", Content);
            _isCopied = true;
            _copyFeedbackTimer.Start();
            StateHasChanged();
        }
        catch (Exception ex)
        {
            await JSRuntime.InvokeVoidAsync("alert", $"Failed to copy: {ex.Message}");
        }
    }

    private void FormatJson()
    {
        try
        {
            var jsonObj = JsonDocument.Parse(Content);
            Content = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });
        }
        catch (JsonException ex)
        {
            JSRuntime.InvokeVoidAsync("alert", $"Invalid JSON: {ex.Message}");
        }
    }

    private void FormatXml()
    {
        try
        {
            var doc = XDocument.Parse(Content);
            Content = doc.ToString();
        }
        catch (System.Xml.XmlException ex)
        {
            JSRuntime.InvokeVoidAsync("alert", $"Error formatting XML: {ex.Message}");
        }
    }

    public void Dispose()
    {
        _copyFeedbackTimer?.Dispose();
    }

    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
}