using MongoDB.Bson;

namespace WuhEatz.Shared.DenpaDB.Models
{
  public class ArchiveContributer
  {
    public ObjectId _id { get; set; }
    public ObjectId User_id { get; set; }
    public required UserProfile User { get; set; }
    public required ArchiverStats Stats { get; set; }
  }
}
