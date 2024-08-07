using Microsoft.AspNetCore.Components;

namespace WizWebComponents.Layout
{
  partial class TabView
  {
    [Parameter] public RenderFragment? ChildContent { get; set; }
  }
}
