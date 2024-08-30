using System.Net.Http.Json;
using WuhEatz.Shared.ExternalDataModels.Twitch;
using Timer = System.Timers.Timer;

namespace WuhEatz.Client.Services
{
  public class ClientTwitchService
  {
    public TimeSpan TimeBetweenChecks { get; init; } = TimeSpan.FromMinutes(10);
    public DenpaTwitchStats DenpaStats { get; private set; }
    WuhClient Wuh { get; init; }

    public ClientTwitchService(WuhClient wuh) { 
      _= Task.Run(ExecuteAsync);
      Wuh = wuh;
    }

    Task ExecuteAsync ()
    {
      Timer workTimer = new Timer(TimeBetweenChecks);
      workTimer.Elapsed += async (sender, e) => await RequestDenpaInfo();
      workTimer.AutoReset = true;
      workTimer.Start();

      _= RequestDenpaInfo();
      return Task.CompletedTask;
    } 

    async Task RequestDenpaInfo()
    {
      DenpaStats = await Wuh.GetFromJsonAsync<DenpaTwitchStats>("/api/DenpaData");
      Console.WriteLine(DenpaStats.IsLive);
    }
  }
}
