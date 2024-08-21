using MongoDB.Bson;
using WuhEatz.ExternalDataModels.Twitch;

namespace WuhEatz.DenpaDB.Models
{
  public class UserProfile
  {
    public ObjectId _id;
    public string Username { get; set; } = "";
    public TwitchUser? TwitchData { get; set; }
  }
}
