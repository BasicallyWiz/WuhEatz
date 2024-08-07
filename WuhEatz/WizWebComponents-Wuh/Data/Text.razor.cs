using Microsoft.AspNetCore.Components;

namespace WizWebComponents.Data
{
  partial class Text
  {
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Size { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? SplatValues { get; set; }
    string TextSizeClass = "wiz-font-body";

    protected override Task OnParametersSetAsync()
    {
      switch (Size)
      {
        case "Body":
          TextSizeClass = "wiz-font-body";
          break;
        case "Subtitle":
          TextSizeClass = "wiz-font-subtitle";
          break;
        case "Title":
          TextSizeClass = "wiz-font-title";
          break;
        case "Title-L":
          TextSizeClass = "wiz-font-title-l";
          break;
        case "Display":
          TextSizeClass = "wiz-font-display";
          break;
        default:
          TextSizeClass = "wiz-font-body";
          break;
      }

      return base.OnParametersSetAsync();
    }
  }
}
