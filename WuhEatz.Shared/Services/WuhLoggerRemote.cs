using System.Text;
using WuhEatz.Shared.DenpaDB.Contexts;

namespace WuhEatz.Shared.Services
{
  public class WuhLoggerRemote
  {
    public HttpClient? client { get; set; }
    public WuhLoggerRemote() { }

    public Task LogDebug(string Data)
    {
      if (client is null) return Task.CompletedTask;
      var content = new StringContent($"\"{Data}\"", Encoding.UTF8, "application/json");
      _= client.PostAsync($"/api/Logging/Debug", content);
      return Task.CompletedTask;
    }
    public Task LogInfo(string Data)
    {
      if (client is null) return Task.CompletedTask;
      var content = new StringContent($"\"{Data}\"", Encoding.UTF8, "application/json");
      _= client.PostAsync($"/api/Logging/Info", content);
      return Task.CompletedTask;
    }
    public Task LogWarn(string Data)
    {
      if (client is null) return Task.CompletedTask;
      var content = new StringContent($"\"{Data}\"", Encoding.UTF8, "application/json");
      _= client.PostAsync($"/api/Logging/Warn", content);
      return Task.CompletedTask;
    }
    public Task LogError(string Data)
    {
      if (client is null) return Task.CompletedTask;
      var content = new StringContent($"\"{Data}\"", Encoding.UTF8, "application/json");
      _= client.PostAsync($"/api/Logging/Error", content);
      return Task.CompletedTask;
    }
  }
}
