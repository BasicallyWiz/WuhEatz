using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WuhEatz.DenpaDB.Models
{
  public class Session
  {
    public ObjectId _id { get; set; }
    public string Code { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ObjectId? Owner_id { get; set; }
    required public UserProfile Owner { get; set; }
  }
}
