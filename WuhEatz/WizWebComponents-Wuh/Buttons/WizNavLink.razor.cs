using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizWebComponents.Buttons
{
  partial class WizNavLink
  {
    [Parameter] public string? href { get; set; }
    [Parameter] public string? icon { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? SplatAttributes { get; set; }
    [Inject] NavigationManager? Nav { get; set; }

    string iconStyle 
    { get
      {
        string styles = "height: 100%; background-size: contain; background-position: center; background-repeat: no-repeat;";

        if (icon is not null)
        {
          styles += $" background-image: url({icon}); height: 3em; aspect-ratio: 1/1;";
        }

        return styles;
      } 
    }

    protected override Task OnParametersSetAsync()
    {
      if (href is not null)
      {
        if (href.StartsWith('?') || href.StartsWith('&'))
        {
          Dictionary<string, string> query = new();
          var thang = href.Replace("?", "").Replace("&", "").Split('=');

          KeyValuePair<string, string> newQuery = new(thang[0], thang[1]);

          if (Nav!.Uri.Contains("?"))
          {
            try
            {
              query = Nav!.Uri.Split('?')[1].Split('&').Select(x => x.Split('=')).ToDictionary(x => x[0], x => x[1]);
            }
            catch (Exception)
            {
              query = new();
            }
          }


          if (Nav!.Uri.Contains('?'))
          {
            href = href.Replace('?', '&');
          }


          if (query.Keys.Any(x => x == newQuery.Key))
          {
            KeyValuePair<string, string> oldQuery = query.First(x => x.Key == href.Replace("?", "").Replace("&", "").Split('=')[0]);
            href = Nav.Uri.Replace($"{oldQuery.Key}={oldQuery.Value}", $"{newQuery.Key}={newQuery.Value}");
          }
          else
          {
            href = Nav!.Uri + href;
          }
        }
      }
      return base.OnParametersSetAsync();
    }
  }
}
