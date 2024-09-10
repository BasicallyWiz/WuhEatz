using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuhEatz.Shared.DenpaDB.Models
{
  public class Log
  {
    public ObjectId _id { get; set; }
    public required string Type { get; set; }
    public required string Data { get; set; }
    public ObjectId? LoggedUser_id { get; set; }
    public UserProfile? LoggedUser { get; set; }
  }
}
