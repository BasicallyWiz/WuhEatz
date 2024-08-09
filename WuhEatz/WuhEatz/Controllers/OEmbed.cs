using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WuhEatz.Controllers
{
  [Route("services/oembed/")]
  [ApiController]
  public class OEmbed : ControllerBase
  {
    [HttpGet]
    public IActionResult Get(string format, string url)
    {
      //  Since there are literally three pages as of now, I don't care how scuffed this code is.
      if (format != "json") return StatusCode(501, "Endpoint only supports json");

      //TODO:  This barely functions, and honestly I have no idea what I'm doing. Please help.

      return Ok(
        """
        {
          "type": "link",
          "title": "Wuh Eatz!!",
          "description": "The Wuh Eatz Website!! Yippee!!",
          "color": 9282159,
          "provider": {
            "name": "WuhEatz",
            "url": "https://wuheatz.ca"
          }
        }
        """
        );
    }
  }
}
