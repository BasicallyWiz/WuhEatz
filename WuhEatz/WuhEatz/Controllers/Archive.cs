using Microsoft.AspNetCore.Mvc;
using WuhEatz.Services;
using WuhEatz.Shared.DenpaDB.Contexts;
using WuhEatz.Shared.DenpaDB.Models;

namespace WuhEatz.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class Archive : ControllerBase
  {
    [HttpGet]
    public IActionResult ListEntries()
    {

      ArchiveContext ctx = ArchiveContext.Create(MongoService.instance?.GetDatabase("DenpaDB")!);


      return Ok();
    }
  }
}
