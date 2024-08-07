using Microsoft.AspNetCore.Components;

namespace WizWebComponents.Buttons
{
    partial class OAuthLink
    {
        [Parameter]
        public string ServiceUrl { get; set; }
        [Parameter]
        public string ServiceName { get; set; }
        [Parameter]
        public string? ServiceIcon { get; set; }
        [Parameter]
        public string ServiceColor { get; set; } = "white";
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? Splat { get; set; }
    }
}
