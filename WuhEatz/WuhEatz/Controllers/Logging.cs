using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WuhEatz.Services;
using WuhEatz.Shared.DenpaDB.Contexts;

namespace WuhEatz.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class Logging : ControllerBase
  {
    WuhLogger logger;
    public Logging(WuhLogger logger)
    {
      this.logger = logger;
    }

    [HttpPost("info")]
    public async Task<IActionResult> Info([FromBody] string data)
    {
      string? session = HttpContext.Request.Cookies["session"];
      if (session is null)
      {
        await logger.LogInfo(data);
        return Ok();
      }
      else
      {
        ProfilesContext ctx = ProfilesContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));
        await logger.LogInfo(data, ctx.Sessions.First(x => x.Code == session).Owner_id);
        return Ok();
      }
    }

    [HttpPost("warn")]
    public async Task<IActionResult> Warn([FromBody] string data)
    {
      string? session = HttpContext.Request.Cookies["session"];
      if (session is null)
      {
        await logger.LogInfo(data);
        return Ok();
      }
      else
      {
        ProfilesContext ctx = ProfilesContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));
        await logger.LogWarn(data, ctx.Sessions.First(x => x.Code == session).Owner_id);
        return Ok();
      }
    }

    [HttpPost("error")]
    public async Task<IActionResult> Error([FromBody] string data)
    {
      string? session = HttpContext.Request.Cookies["session"];
      if (session is null)
      {
        await logger.LogInfo(data);
        return Ok();
      }
      else
      {
        ProfilesContext ctx = ProfilesContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));
        await logger.LogError(data, ctx.Sessions.First(x => x.Code == session).Owner_id);
        return Ok();
      }
    }

    [HttpPost("debug")]
    public async Task<IActionResult> Debug([FromBody] string data)
    {
      string? session = HttpContext.Request.Cookies["session"];
      if (session is null)
      {
        await logger.LogInfo(data);
        return Ok();
      }
      else
      {
        ProfilesContext ctx = ProfilesContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));
        await logger.LogDebug(data, ctx.Sessions.First(x => x.Code == session).Owner_id);
        return Ok();
      }
    }
  }
}
