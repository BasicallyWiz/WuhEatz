using Microsoft.AspNetCore.Components;
using WuhEatz.Client.Services;

namespace WuhEatz.Client.Components.Pages
{
  partial class Login
  {
    [Inject] ClientTwitchService? TwitchService { get; set; }
    [Inject] NavigationManager? Nav { get; set; }
    [Inject] WuhClient? Wuh { get; set; }

    string AuthUrl { get; set; } = "";

    protected override async Task OnParametersSetAsync()
    {
      if (Nav is not null)
      {
        AuthUrl = await Wuh?.GetStringAsync("/api/TwitchData/AuthUrl")! ?? "";
      }

      await base.OnParametersSetAsync();
    }
  }
}
