using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WizWebComponents.Services;

namespace WuhEatz.Middleware
{
  public class ProfileLogin
  {
    public async Task InvokeAsync(NavigationManager Nav, HttpClient client, IJSRuntime JS, CookieList cookies)
    {
      // Reference code structure from https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-8.0#code-try-2

      Dictionary<string, string>? query = new();
      if (Nav.Uri.Contains('?')) query = new(Nav.Uri.Split('?')[1].Split('&').Select(x => new KeyValuePair<string, string>(x.Split('=')[0], x.Split('=')[1])));

      string? accessCode;
        query.TryGetValue("code", out accessCode);
      string? accessScope;
        query.TryGetValue("scope", out accessScope);
      string? accessState;
        query.TryGetValue("state", out accessState);

      Console.WriteLine("sessioncookie: " + cookies["session"]);

      // Check if user is trying to log in. Ff not, check if the user is already logged in.
      if (accessCode is not null && accessScope is not null)
      {
        //  TODO: send access code and scope to a controller for the server to handle the rest of OAuth.
        var result = await client.PostAsync($"{Nav.BaseUri}api/TwitchLogin?AccessCode={accessCode}&AccessScope={accessScope}", null);

        cookies.Add(new Cookie("session", await result.Content.ReadAsStringAsync(), Nav.BaseUri.Split(':')[1].Replace("/", ""), DateTime.Now.AddMonths(1)));

      }
      else if (cookies["session"] is not null)
      {
        Console.WriteLine("Validating Cookies...");
        await client.GetAsync($"{Nav.BaseUri}api/TwitchLogin");
        //  TODO: Request a validation check from the server to see if the session is still valid.
        //  If the session is valid, uhh... I forgor. I think we can do nothing.
        //  If the session is invalid, redirect to the login page.
      }
      //  If neither of the above are true, do nothing.

      //await _next(context);
    }
  }
}
