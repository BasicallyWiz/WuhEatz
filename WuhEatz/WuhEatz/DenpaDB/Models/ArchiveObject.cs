//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;
//
//namespace WuhEatz.DenpaDB.Models
//{
//  public class ArchiveObject
//  {
//    public ObjectId _id { get; set; }
//    public string ResourceUrl { get; set; } = "";
//    public string ResourceType { get; set; } = "";
//    public ObjectId Uploader_id { get; set; }
//    required public UserProfile Uploader { get; set; }
//
//    public Dictionary<string, object> Data { get; set; } = new();
//  }
//}
//