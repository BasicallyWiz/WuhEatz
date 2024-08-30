//  TODO: This file is being executed on the server. Move this to the client.
//  Having this on the server might cause loopback issues, plus, it's generally just more elegant.
namespace WuhEatz.Middleware
{
  public class ProfileLogin
  {
    readonly RequestDelegate _next;

    public ProfileLogin(RequestDelegate next) { 
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context, HttpClient client)
    {
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
      }
      else if (userSession is not null)
      {
        //  TODO: Request a validation check from the server to see if the session is still valid.
        //  If the session is valid, uhh... I forgor. I think we can do nothing.
        //  If the session is invalid, redirect to the login page.
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
