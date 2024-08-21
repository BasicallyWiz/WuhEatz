using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WuhEatz.ExternalDataModels.Twitch;

namespace WuhEatz.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TwitchLogin : ControllerBase
  {
    [HttpPost]
    public async Task<IActionResult> GetNewLogin(string AccessCode, string AccessScope)
    {
      HttpClient client = new HttpClient();
      var response = await client.PostAsync("https://id.twitch.tv/oauth2/token", new FormUrlEncodedContent(new Dictionary<string, string>
      {
        { "client_id", System.IO.File.ReadAllLines("TwitchApi.token")[0] },
        { "client_secret", System.IO.File.ReadAllLines("TwitchApi.token")[1] },
        { "code", AccessCode },
        { "grant_type", "authorization_code" },
        { "redirect_uri", $"https://{Request.Host}/" }
      }));

      TwitchOAuthAccessInfo info = await response.Content.ReadFromJsonAsync<TwitchOAuthAccessInfo>();
      if (info.access_token is null) return BadRequest(new { title="BAD_DATA", message="Something went wrong with the data you gave us, and Twitch didn't give us any data." });

      client.DefaultRequestHeaders.Add("Authorization", $"Bearer {info.access_token}");
      client.DefaultRequestHeaders.Add("Client-Id", System.IO.File.ReadAllLines("TwitchApi.token")[0]);

      var result = await client.GetAsync("https://api.twitch.tv/helix/users");
      UsersQueryData users = await result.Content.ReadFromJsonAsync<UsersQueryData>();
      if (users.data.Length <= 0) return BadRequest(new { title="USER_DOESNT_EXIST", message="We got pretty far into OAuth, but querying for your data returned nothing. Do you exist?" });
      TwitchUser user = users!.data.First();

      if ((user.AccountAge.TotalDays / 365) < 1) return Unauthorized(new { title = "ACCOUNT_TOO_YOUNG", message = "For spam protection reasons, you cannot sign in with a Twitch account less than ONE YEAR old." });
      
      //TODO: Now that we've got Twitch account info, and verified that it's pretty damn likely not to be a spam account, let's now create a profile useable on WuhEatz.
      //var DBctx = ProfilesContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));

      return Ok();
    }

    //[HttpGet]
    //public async Task<IActionResult> GetExistingLogin()
    //{
    //
    //}

    public struct UsersQueryData
    {
      public TwitchUser[] data { get; set; }
    }
  }
}
