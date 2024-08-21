using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace WuhEatz.Client
{
  internal class Program
  {
    static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);

      WebAssemblyHost app = builder.Build();
      
      await app.RunAsync();
    }
  }
}
