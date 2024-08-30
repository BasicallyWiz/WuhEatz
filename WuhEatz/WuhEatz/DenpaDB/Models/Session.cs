using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WuhEatz.DenpaDB.Models
{
  public class Session
  {
    public ObjectId _id { get; set; }
    public string Code { get; set; } = Guid.NewGuid().ToString();
    public DateTime ExpiresAt { get; set; } = DateTime.Now.AddDays(10);
    public ObjectId? Owner_id { get; set; }
    required public UserProfile Owner { get; set; }
  }
}
