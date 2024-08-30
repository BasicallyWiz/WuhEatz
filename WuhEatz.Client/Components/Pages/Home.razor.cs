using Microsoft.AspNetCore.Components;
using WuhEatz.Client.Services;

namespace WuhEatz.Client.Components.Pages
{
  partial class Home
  {
    [Inject] public ClientTwitchService? TwitchService { get; set; }
    [Inject] public NavigationManager? Nav { get; set; }

    protected override void OnInitialized()
    {
      
    }
  }
}
