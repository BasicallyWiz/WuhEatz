using Microsoft.AspNetCore.Mvc;
using WuhEatz.Shared.DenpaDB.Contexts;
using WuhEatz.Shared.DenpaDB.Models;
using WuhEatz.Services;

namespace WuhEatz.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class Users : ControllerBase
  {
    [HttpGet("list/{page:int}/")]
    public IActionResult ListUsers(int page, int itemsPerPage = 20)
    {
      var ctx = ProfilesContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));
      string session = Request.Cookies["session"] ?? "";

      var sessionData = ctx.Sessions.FirstOrDefault(x => x.Code == session);
      if (sessionData is null) return Unauthorized("INVALID_SESSION");
      Console.WriteLine("user is: " + ctx.Profiles.FirstOrDefault(x => x._id == sessionData.Owner_id)?.TwitchData.login);
      if (ctx.Profiles.FirstOrDefault(x => x._id == sessionData.Owner_id)?.TwitchData.login != "basicallywiz") return Unauthorized("USER_NOT_WIZ");
  
      List<UserProfile> users = ctx.Profiles.Skip(page * itemsPerPage).Take(itemsPerPage).ToList();

      return Ok(users);
    }
  }
}
