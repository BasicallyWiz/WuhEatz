using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Timer = System.Timers.Timer;

namespace WuhEatz.Services
{
  public class TwitchService
  {
    public TimeSpan TimeBetweenChecks { get; init; } = TimeSpan.FromSeconds(60);
    public DenpaTwitchStats DenpaStats { get; private set; }

    public string clientId { get; init; } = "";
    public string clientSecret { get; init; } = "";
    public string grantType { get; init; } = "client_credentials";

    bool IsTwitchEnabled { get; set; } = false;
    HttpClient client { get; set; }
    TwitchToken token { get; set; }

    public TwitchService()
    {
      client = new HttpClient();

      string[] clientInfo = new string[2];
      if (File.Exists("TwitchApi.token")) {
        clientInfo = File.ReadAllLines("TwitchApi.token");
      }
      else
      {
        Console.WriteLine("TwitchApi.token not found. Creating dummy file.");
        File.WriteAllLines("TwitchApi.token", new string[] { "clientid", "clientsecret" });
      }

      clientId = clientInfo[0];
      clientSecret = clientInfo[1];

      _ = Task.Run(ExecuteAsync);
    }

    async Task ExecuteAsync()
    {
      await GetAccessToken();
      if (!IsTwitchEnabled) return;

      Timer workTimer = new Timer(TimeBetweenChecks);
      workTimer.Elapsed += async (sender, e) => await DoWork();
      workTimer.AutoReset = true;
      workTimer.Start();

      //  Twitch API requires a token validation every hour
      Timer validateTimer = new Timer(TimeSpan.FromHours(1));
      validateTimer.Elapsed += async (sender, e) => await ValidateToken();
      validateTimer.AutoReset = true;
      validateTimer.Start();

      await ValidateToken();
      await DoWork();
    }
    async Task GetAccessToken()
    {
      try {
        var result = await client.PostAsync($"https://id.twitch.tv/oauth2/token?client_id={clientId}&client_secret={clientSecret}&grant_type={grantType}", null);
        if (!result.IsSuccessStatusCode)
        {
          Console.WriteLine("Failed to get access token from Twitch. Twitch integrations are disabled.");
          return;
        }
        IsTwitchEnabled = true;

        string res = await result.Content.ReadAsStringAsync();
        token = JsonSerializer.Deserialize<TwitchToken>(res);

        client.DefaultRequestHeaders.Remove("Authorization");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.access_token}");
        client.DefaultRequestHeaders.Remove("Client-Id");
        client.DefaultRequestHeaders.Add("Client-Id", clientId);
      }
      catch (Exception ex) { 
        Console.WriteLine(ex);
        IsTwitchEnabled = false; 
      }
    }
    async Task ValidateToken()
    {
      var result = await client.GetAsync($"https://id.twitch.tv/oauth2/validate");

      if (result.IsSuccessStatusCode)
      {
        return;
      }
      else
      {
        Console.WriteLine("TwitchService.ValidateToken() failed. Switching IsTwitchEnabled to false...");
        Console.WriteLine(result.Content.ReadAsStringAsync().Result);
        IsTwitchEnabled = false;
        return;
      }
    }
    async Task DoWork()
    {
      if (IsTwitchEnabled == false)
      {
        // Try to get a new token
        await GetAccessToken();

        // If we still can't get a token, return. Another attempt will be made after TimeBetweenChecks
        if (IsTwitchEnabled == false)
        {
          return;
        }
      }
      try
      {
        // 779607673 <- This is Denpa's user ID in case we ever need that
        var result = await client.GetAsync($"https://api.twitch.tv/helix/streams?user_login=snoozy"); //  Set to snoozy because at the time of writing, she's live.

        string StringResult = await result.Content.ReadAsStringAsync();
        Console.WriteLine(StringResult);

        //  TODO: Compile the result of this to the DenpaTwitchStats struct, and populate DenpaStats, so we can [hopefully] inject the data into pages
        //  Honestly I've been inplementing this all based on a guess this will work...
      }
      catch (Exception ex)
      {
        Console.WriteLine("Something happened in TwitchService.DoWork(), Switching IsTwitchEnabled to false...");
        Console.WriteLine(ex);
        IsTwitchEnabled = false;
      }
    }
  }

  public struct DenpaTwitchStats
  {
    //  TODO: Create a struct that can be populated by querying the https://api.twitch.tv/helix/streams?user_login=denpafish endpoint.
    //  Might have to query other streamers to get the return data, because Denpa's not gonna be live any time soon...
  }

  public struct TwitchToken
  {
    public string access_token { get; set; }
    public uint expires_in { get; set; }
    public string token_type { get; set; }
  }
}