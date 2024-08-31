using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WuhEatz.Shared.ExternalDataModels.Twitch;

namespace WuhEatz.DenpaDB.Models
{
  public class UserProfile
  {
    public ObjectId _id { get; set; }
    public string Username { get; set; } = "";
    public required TwitchUser TwitchData { get; set; }
    public required TwitchOAuthAccessInfo Auth { get; set; }
    public SubscriptionData? SubData { get; set; }
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
    public int DenpaSubTeir { get; set; } = 0;
  }
}
