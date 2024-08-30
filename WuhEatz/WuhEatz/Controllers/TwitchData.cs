using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WuhEatz.Services;

namespace WuhEatz.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TwitchData : ControllerBase
  {
    TwitchService TwitchService;

    public TwitchData(TwitchService twitchService)
    {
      this.TwitchService = twitchService;
    }

    [HttpGet("/Denpa")]
    public IActionResult GetDenpaData()
    {
      return Ok(TwitchService.DenpaStats);
    }

    [HttpGet("/AuthUrl")]
    public IActionResult GetAuthUrl()
    {
      return Ok(TwitchService.GetAuthUrl($"https://{Request.Host.ToString()}"));
    }
  }
}
