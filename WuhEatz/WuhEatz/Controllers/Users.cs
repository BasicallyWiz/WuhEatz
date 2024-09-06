using Microsoft.AspNetCore.Mvc;
using WuhEatz.Shared.DenpaDB.Contexts;
using WuhEatz.Shared.DenpaDB.Models;
using WuhEatz.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WuhEatz.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class Users : ControllerBase
  {
    JsonSerializerOptions JsonOptions = new() 
    {
      ReferenceHandler = ReferenceHandler.IgnoreCycles
    };
    WuhLogger logger;

    public Users(WuhLogger logger)
    {
      this.logger = logger;
    }

    [HttpGet("list/{page:int}/")]
    public IActionResult ListUsers(int page, int itemsPerPage = 20)
    {
      var ctx = ProfilesContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));
      string session = Request.Cookies["session"] ?? "";

      var sessionData = ctx.Sessions.FirstOrDefault(x => x.Code == session);
      if (sessionData is null) return Unauthorized("INVALID_SESSION");
      var currentUser = ctx.Profiles.FirstOrDefault(x => x._id == sessionData.Owner_id);
      logger.LogInfo($"User '{currentUser?.Username ?? "NONE"}' with id '{currentUser?._id}' just opened the admin page.");
      if (currentUser?.TwitchData.login != "basicallywiz") return Unauthorized("USER_NOT_WIZ");
  
      List<UserProfile> users = ctx.Profiles.Skip(page * itemsPerPage).Take(itemsPerPage).ToList();

      string ReturnData = JsonSerializer.Serialize(users, JsonOptions);
      return Ok(ReturnData);
    }
  }
}
