using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using WizWebComponents.Services;
using WuhEatz.Middleware;
using WuhEatz.Shared.Services;

namespace WuhEatz.Client
{
  internal class Program
  {
    public static WuhLoggerRemote? logger;

    static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);

      builder.Services.AddScoped(x => new WuhLoggerRemote());
      builder.Services.AddScoped(x => new HttpClient());
      builder.Services.AddScoped(x => new ProfileLogin());
      builder.Services.AddScoped(x => new CookieList(x.GetRequiredService<IJSRuntime>(), "window.CookieAccessor.get", "window.CookieAccessor.set"));

      WebAssemblyHost app = builder.Build();

      var cookies = app.Services.GetRequiredService<CookieList>();
      var Nav = app.Services.GetRequiredService<NavigationManager>();
      var Client = app.Services.GetRequiredService<HttpClient>();
      var JS = app.Services.GetRequiredService<IJSRuntime>();

      Client.BaseAddress = new Uri(Nav.BaseUri);
      await cookies.PopulateFromCookies(JS);
      
      var _logger = app.Services.GetRequiredService<WuhLoggerRemote>();
      _logger.client = Client;
      logger = _logger;

      await app.Services.GetRequiredService<ProfileLogin>().InvokeAsync(Nav, Client, JS, cookies);

      await _logger.LogDebug("Wasm has started up!");
      await app.RunAsync();
    }
  }
}
