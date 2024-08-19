using System.Runtime.CompilerServices;
using WuhEatz.Components;
using WuhEatz.Services;

namespace WuhEatz
{
  public class Program
  {
    public static MongoService? mongoService;

    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddRazorComponents()
          .AddInteractiveServerComponents()
          .AddInteractiveWebAssemblyComponents();

      builder.Services.AddControllers();


      var twtchsvc = new TwitchService()
      {
        //  TODO: When we are no longer testing the twitch service, set the timespan to something longer. Probably best to do 5 minutes.
        TimeBetweenChecks = TimeSpan.FromMinutes(5)
      };
      builder.Services.AddScoped(x => twtchsvc);

      MongoService.instance = new();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseWebAssemblyDebugging();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
        app.UseHttpsRedirection();
      }

      app.MapControllers();

      app.UseStaticFiles();
      app.UseAntiforgery();

      app.MapRazorComponents<App>()
         .AddInteractiveServerRenderMode()
         .AddInteractiveWebAssemblyRenderMode()
         .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

      /**
       * System.Net.Sockets.SocketException: 'The requested address is not valid in its context.'
       * Go to ./Properties/launchSettings.json and change "applicationUrl" for http and https to addresses available to you.
       * This error throws when ASPNet core tries to listen to a used or inaccessible address.
       */
      app.Run();
    }
  }
}
