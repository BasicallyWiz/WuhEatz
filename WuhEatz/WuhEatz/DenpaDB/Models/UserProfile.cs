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
    public TwitchUser? TwitchData { get; set; }
    public TwitchOAuthAccessInfo? Auth { get; set; }
    public virtual ICollection<Session>? Sessions { get; set; }
  }
}
