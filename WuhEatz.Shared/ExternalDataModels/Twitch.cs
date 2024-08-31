/// <summary>
/// This file contains the data models for the Twitch API.
/// </summary>
namespace WuhEatz.Shared.ExternalDataModels.Twitch;

public class TwitchOAuthAccessInfo
{
  public required string access_token { get; set; }
  public required uint expires_in { get; set; }
  public required string refresh_token { get; set; }
  public required string[] scope { get; set; }
  public required string token_type { get; set; }
}

public class TwitchUser
{
  public required string id { get; set; }
  public required string login { get; set; }
  public required string display_name { get; set; }
  public required string type { get; set; }
  public required string broadcaster_type { get; set; }
  public required string description { get; set; }
  public string? profile_image_url { get; set; }
  public string? offline_image_url { get; set; }
  public uint? view_count { get; set; }
  public string? email { get; set; }
  public DateTime? created_at { get; set; }
  public TimeSpan? AccountAge { get { return DateTime.Now - created_at; } }
}

public class TokenData
{
  public required string access_token { get; set; }
  public required uint expires_in { get; set; }
  public required string token_type { get; set; }
}

public class StreamData
{
  public required string id { get; set; }
  public required string user_id { get; set; }
  public required string user_login { get; set; }
  public required string user_name { get; set; }
  public string? game_id { get; set; }
  public string? game_name { get; set; }
  /// <remarks>
      /// I've only ever seen this parameter be "live" before, but i guess expect other possibilities...
      /// </remarks>
  public string? type { get; set; }
  public bool isLive { get { if (type == "live") return true; else return false; } }
  public required string title { get; set; }
  public required ulong viewer_count { get; set; }
  public required DateTime started_at { get; set; }
  public required string language { get; set; }
  public required string thumbnail_url { get; set; }
  public string[]? tag_ids { get; set; } // I don't actually know what this is
  public string[]? tags { get; set; }
  public bool is_mature { get; set; } = false;
}

public class Subscriptions
{
  public SubscriptionData[] data { get; set; }
}
public class SubscriptionData
{
  public required string broadcaster_id { get; set; }
  public required string broadcaster_name { get; set; }
  public required string broadcaster_login { get; set; }
  public required string tier { get; set; }
  public bool is_gift { get; set; }
  public string? gifter_id { get; set; }
  public string? gifter_login { get; set; }
  public string? gifter_name { get; set; }
}