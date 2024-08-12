using Microsoft.AspNetCore.Components;
using WuhEatz.Services;

namespace WuhEatz.Components.Pages
{
  partial class Home
  {
    [Inject] public TwitchService? TwitchService { get; set; }
    [Inject] public NavigationManager? Nav { get; set; }

    protected override void OnInitialized()
    {
      
    }
  }
}
