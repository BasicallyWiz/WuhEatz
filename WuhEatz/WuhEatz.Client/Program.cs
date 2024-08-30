using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using WizWebComponents.Services;
using WuhEatz.Middleware;

namespace WuhEatz.Client
{
  internal class Program
  {
    static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);

      builder.Services.AddScoped(x => new HttpClient());
      builder.Services.AddScoped(x => new ProfileLogin());
      builder.Services.AddScoped(x => new CookieList(x.GetRequiredService<IJSRuntime>(), "window.CookieAccessor.get", "window.CookieAccessor.set"));

      WebAssemblyHost app = builder.Build();
      var cookies = app.Services.GetRequiredService<CookieList>();

      var Nav = app.Services.GetRequiredService<NavigationManager>();
      var Client = app.Services.GetRequiredService<HttpClient>();
      var JS = app.Services.GetRequiredService<IJSRuntime>();

      await cookies.PopulateFromCookies(JS);
      await app.Services.GetRequiredService<ProfileLogin>().InvokeAsync(Nav, Client, JS, cookies);

      Console.WriteLine("Wasm has started up!");
      await app.RunAsync();
    }
  }
}
