namespace WuhEatz.Middleware
{
  public class ProfileLogin
  {
    readonly RequestDelegate _next;

    public ProfileLogin(RequestDelegate next) { 
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      // Reference code structure from https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-8.0#code-try-2

      string? accessCode = context.Request.Query["code"];
      string? accessScope = context.Request.Query["scope"];
      string? userSession = context.Request.Cookies["session"];

      // Check if user is trying to log in. Ff not, check if the user is already logged in.
      if (accessCode is not null && accessScope is not null)
      {
        //  TODO: send access code and scope to a controller for the server to handle the rest of OAuth.
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
}
