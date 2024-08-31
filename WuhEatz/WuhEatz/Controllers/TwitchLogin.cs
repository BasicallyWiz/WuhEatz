using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WuhEatz.DenpaDB.Contexts;
using WuhEatz.DenpaDB.Models;
using WuhEatz.Shared.ExternalDataModels.Twitch;
using WuhEatz.Services;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

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

      TwitchOAuthAccessInfo? info = await response.Content.ReadFromJsonAsync<TwitchOAuthAccessInfo>();
      if (info?.access_token is null) return BadRequest(new { title="BAD_DATA", message="Something went wrong with the data you gave us, and Twitch didn't give us any data." });

      client.DefaultRequestHeaders.Add("Authorization", $"Bearer {info.access_token}");
      client.DefaultRequestHeaders.Add("Client-Id", System.IO.File.ReadAllLines("TwitchApi.token")[0]);

      var result = await client.GetAsync("https://api.twitch.tv/helix/users");
      UsersQueryData users = await result.Content.ReadFromJsonAsync<UsersQueryData>();
      if (users.data.Length <= 0) return BadRequest(new { title="USER_DOESNT_EXIST", message="We got pretty far into OAuth, but querying for your data returned nothing. You do exist, right?" });
      TwitchUser user = users!.data.First();

      if ((((TimeSpan)user.AccountAge!).TotalDays / 365) < 1) return Unauthorized(new { title = "ACCOUNT_TOO_YOUNG", message = "For spam protection reasons, you cannot sign in with a Twitch account less than ONE YEAR old." });
      
      var res = await client.GetAsync($"https://api.twitch.tv/helix/subscriptions/user?broadcaster_id=779607673&user_id={user.id}");

      Subscriptions? SubData;
      var SubResult = await client.GetAsync($"https://api.twitch.tv/helix/subscriptions/user?broadcaster_id=779607673&user_id={user.id}");
      if (SubResult.IsSuccessStatusCode)
      {
        SubData = await SubResult.Content.ReadFromJsonAsync<Subscriptions>();
      }
      else SubData = null;

      var DenpaDB = MongoService.instance!.GetDatabase("DenpaDB");
      var DBctx = ProfilesContext.Create(DenpaDB);

      var NewUser = new UserProfile()
      {
        _id = ObjectId.GenerateNewId(),
        Username = user.display_name ?? user.login,
        TwitchData = user,
        SubData = SubData?.data.First(x => x.broadcaster_login == "denpafish"),
        Auth = info
      };

      var NewSession = new Session()
      {
        _id = ObjectId.GenerateNewId(),
        Owner = NewUser
      };

      DBctx.Profiles.Add(NewUser);
      DBctx.Sessions.Add(NewSession);

      DBctx.SaveChanges();

      return Ok(NewSession.Code);
    }

    [HttpGet]
    public async Task<IActionResult> ValidateSession()
    {
      string? session = Request.Cookies["session"];
      if (session is null) return Unauthorized(new { title = "NO_SESSION", message = "You need a session to validate a session." });
    
      var ctx = ProfilesContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));
    
      var DBSession = ctx.Sessions.FirstOrDefault(x => x.Code == session);
      if (DBSession is null) return Unauthorized(new { title = "INVALID_SESSION", message = "The session you provided is invalid." });
      if (DateTime.Now > DBSession.ExpiresAt) return Unauthorized(new { title = "SESSION_EXPIRED", message = "The session you provided has expired." });

      UserProfile? DBUser = ctx.Profiles.FirstOrDefault(x => x._id == DBSession.Owner_id);
      if (DBUser is null) return Unauthorized(new { title = "USER_DOESNT_EXIST", message = "Oddly, Your session is in the database, but there's no user attached..." });

      HttpClient client = new HttpClient();
      client.DefaultRequestHeaders.Add("Authorization", $"Bearer {DBSession.Owner.Auth.access_token}");
      client.DefaultRequestHeaders.Add("Client-Id", System.IO.File.ReadAllLines("TwitchApi.token")[0]);
      var result = await client.GetFromJsonAsync<Subscriptions>($"https://api.twitch.tv/helix/subscriptions/user?broadcaster_id=779607673&user_id={DBUser.TwitchData.id}");

      DBUser.SubData = result?.data[0];
      ctx.Profiles.Update(DBUser);
      ctx.SaveChanges();

      return Ok($"Welcome back, {DBUser.Username}");
    }

    public struct UsersQueryData
    {
      public TwitchUser[] data { get; set; }
    }
  }
}
