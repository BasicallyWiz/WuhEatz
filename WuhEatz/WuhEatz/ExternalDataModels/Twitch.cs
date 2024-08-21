/// <summary>
/// This file contains the data models for the Twitch API.
/// </summary>
namespace WuhEatz.ExternalDataModels.Twitch;

public struct TwitchOAuthAccessInfo
{
  public string access_token { get; set; }
  public uint expires_in { get; set; }
  public string refresh_token { get; set; }
  public string[] scope { get; set; }
  public string token_type { get; set; }
}

public struct TwitchUser
{
  public string id { get; set; }
  public string login { get; set; }
  public string display_name { get; set; }
  public string type { get; set; }
  public string broadcaster_type { get; set; }
  public string description { get; set; }
  public string profile_image_url { get; set; }
  public string offline_image_url { get; set; }
  public uint view_count { get; set; }
  public string? email { get; set; }
  public DateTime created_at { get; set; }
  public TimeSpan AccountAge { get { return DateTime.Now - created_at; } }
}

public struct TokenData
{
  public string access_token { get; init; }
  public uint expires_in { get; init; }
  public string token_type { get; init; }
}

public struct StreamData
{
      public string id { get; init; }
      public string user_id { get; init; }
      public string user_login { get; init; }
      public string user_name { get; init; }
      public string game_id { get; init; }
      public string game_name { get; init; }
      /// <remarks>
      /// I've only ever seen this parameter be "live" before, but i guess expect other possibilities...
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