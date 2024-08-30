//  TODO: This file is being executed on the server. Eventually move this to the client.
//  Having this on the server might cause loopback issues, plus, it's generally just more elegant.

//  Hi, this might be way more difficult than expected. I can't manage to make code work on the client. I can't seem to make any code run in the client project...
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace WuhEatz.Middleware
{
  public class ProfileLogin
  {
    private readonly RequestDelegate _next;

    public ProfileLogin(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, HttpClient client, IJSRuntime JS)
    {
      //Dictionary<string, string> query = new (Nav.Uri.Split('?')[1]
      //                                      .Split('&')
      //                                      .Select(x => new KeyValuePair<string, string>(
      //                                        x.Split('=')[0], 
      //                                        x.Split('=')[1])
      //                                      )
      //);

      // Reference code structure from https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-8.0#code-try-2

      string? accessCode = context.Request.Query["code"];
      string? accessScope = context.Request.Query["scope"];
      string? accessState = context.Request.Query["state"];
      string? userSession = context.Request.Cookies["session"];

      // Check if user is trying to log in. Ff not, check if the user is already logged in.
      if (accessCode is not null && accessScope is not null)
      {
        //  TODO: send access code and scope to a controller for the server to handle the rest of OAuth.
        var result = await client.PostAsync($"https://{context.Request.Host}/api/TwitchLogin?AccessCode={accessCode}&AccessScope={accessScope}", null);

        var session = await result.Content.ReadAsStringAsync();

        
      }
      else if (userSession is not null)
      {
        //  TODO: Request a validation check from the server to see if the session is still valid.
        //  If the session is valid, it's safe to take no action.
        //  If the session is invalid, delete our session key.
      }
      //  If neither of the above are true, do nothing.

      await _next(context);
    }
  }
  public static class ProfileLoginExtensions
  {
    public static IApplicationBuilder UseProfileLogin(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ProfileLogin>();
    }
  }
}
