using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
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
    string clientSecret { get; init; } = "";
    string grantType { get; init; } = "client_credentials";

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

    //TODO: After five consecutive failed attempts to get a token, disable Twitch integrations, and disable timers.
    async Task ExecuteAsync()
    {
      await GetAccessToken();
      if (!IsTwitchEnabled) return;

      Timer workTimer = new Timer(TimeBetweenChecks);
      workTimer.Elapsed += async (sender, e) => await CheckDenpaLive();
      workTimer.AutoReset = true;
      workTimer.Start();

      //  Twitch API requires a token validation every hour
      Timer validateTimer = new Timer(TimeSpan.FromHours(1));
      validateTimer.Elapsed += async (sender, e) => await ValidateToken();
      validateTimer.AutoReset = true;
      validateTimer.Start();

      await ValidateToken();
      await CheckDenpaLive();
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
    async Task CheckDenpaLive()
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
        var result = await client.GetAsync($"https://api.twitch.tv/helix/streams?user_login=denpafish"); //  Set to others because at the time of writing, Denpa's not live

        string results = await result.Content.ReadAsStringAsync();
        DenpaStats = JsonSerializer.Deserialize<DenpaTwitchStats>(results);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Something happened in TwitchService.DoWork(), Switching IsTwitchEnabled to false...");
        Console.WriteLine(ex);
        IsTwitchEnabled = false;
      }
    }

    public string GetAuthUrl(NavigationManager Nav)
    {
      return $"https://id.twitch.tv/oauth2/authorize?client_id={clientId}&redirect_uri={Nav.BaseUri}&response_type=code&scope=user:read user:read:subscriptions";
    }
  }

  public struct DenpaTwitchStats
  {
    public Data[] data { get; init; }
    public Pagination? pagination { get; init; }
    public bool IsLive
    {
      get { if (data is not null) return data.Any(x => x.isLive); return false; }
    }
    public Data? LiveData
    {
      get { return data.First(x => x.isLive); }
    }
  }

  public struct Data
    {
      public string id { get; init; }
      public string user_id { get; init; }
      public string user_login { get; init; }
      public string user_name { get; init; }
      public string game_id { get; init; }
      public string game_name { get; init; }
      /// <remarks>
      /// I've seen this parameter be "live" before, but i guess expect other possibilities...
      /// </remarks>
      public string type { get; init; }
      public bool isLive { get { if (type == "live") return true; else return false; } }
      public string title { get; init; }
      public ulong viewer_count { get; init; }
      public DateTime started_at { get; init; }
      public string language { get; init; }
      public string thumbnail_url { get; init; }
      public string[]? tag_ids { get; init; } // I don't actually know what this is
      public string[]? tags { get; init; }
      public bool is_mature { get; init; }
    }
  public struct Pagination
    {
      public string cursor { get; internal set; }
    }

  public struct TwitchToken
  {
    public string access_token { get; init; }
    public uint expires_in { get; init; }
    public string token_type { get; init; }
  }
}