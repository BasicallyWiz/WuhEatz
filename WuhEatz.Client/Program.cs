using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WuhEatz.Client.Services;

namespace WuhEatz.Client
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("#app");
      builder.RootComponents.Add<HeadOutlet>("head::after");

      var wuh = new WuhClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

      builder.Services.AddScoped(sp => wuh);
      builder.Services.AddScoped(sp => new ClientTwitchService(wuh));

      await builder.Build().RunAsync();
    }
  }
}
