using Microsoft.AspNetCore.Components;
using WuhEatz.Services;

namespace WuhEatz.Components.Pages
{
  partial class Login
  {
    [Inject] TwitchService? TwitchService { get; set; }
    [Inject] NavigationManager? Nav { get; set; }

    string AuthUrl { get; set; } = "";

    protected override Task OnParametersSetAsync()
    {
      if (Nav is not null)
      {
        AuthUrl = TwitchService?.GetAuthUrl(Nav) ?? "";
      }

      return base.OnParametersSetAsync();
    }
  }
}
