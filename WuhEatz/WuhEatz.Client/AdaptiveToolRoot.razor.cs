using Microsoft.AspNetCore.Components;
using WuhEatz.Shared.Services;

namespace WuhEatz.Client
{
  partial class AdaptiveToolRoot
  {
    static AdaptiveToolRoot? Instance { get; set; }
    [Inject] NavigationManager? Nav { get; set; }
    [Inject] WuhLoggerRemote? Logger { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? Splat { get; set; }
    RenderFragment? ChildContent { get; set; }


    protected override async Task OnParametersSetAsync()
    {
      Nav!.LocationChanged += Nav_LocationChanged;

      await base.OnParametersSetAsync();
      Instance = this;
    }

    private void Nav_LocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
    {
      Logger?.LogDebug("Check if source is still available");
    }
  }
}
