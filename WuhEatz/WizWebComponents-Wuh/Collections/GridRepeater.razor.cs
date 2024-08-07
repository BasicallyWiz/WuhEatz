using Microsoft.AspNetCore.Components;

namespace WizWebComponents.Collections
{
  partial class GridRepeater
  {
    [Parameter] public int ItemsPerLine { get; set; } = 3;
    [Parameter] public bool IsVertical { get; set; } = true;
    [Parameter] public RenderFragment? ChildContent { get; set; }
  }
}
