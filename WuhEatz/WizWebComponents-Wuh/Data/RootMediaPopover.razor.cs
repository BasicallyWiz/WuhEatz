using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WizWebComponents.Data
{

  partial class RootMediaPopover
  {
    [Inject] NavigationManager? Nav { get; set; }
    [Inject] IJSRuntime? JSRuntime { get; set; }

    

    bool isHidden { get; set; } = true;
    string popoverStyle { get
      {
        string style = "position: fixed; top: 0px; width: 80rem; height: 80vh; max-width: calc(100vw - 40px); margin-top: 10vh; display: flex; flex-direction: column;";
        if (isHidden)
        {
          style += "visibility: hidden; pointer-events: none;";
        }
        else
        {
          style += "";
        }
        return style;
      }
    }

    [Parameter] public RenderFragment? ChildContent { get; set; }
    bool IsImage { get; set; }
    bool IsVideo { get; set; }
    string? MediaSource { get; set; }

    protected override async Task OnParametersSetAsync()
    {
      if (Nav is not null)
      {
        Nav.LocationChanged += ((object? _, LocationChangedEventArgs args) => { _ = TestNewUrl(args.Location); });
        await TestNewUrl(Nav.Uri);
      }

      await base.OnParametersSetAsync();
    }

    async Task TestNewUrl(string location)
    {
      Dictionary<string, string> query = new();
      if (location.Contains("?"))
      {
        try
        {
          query = location.Split("?")[1].Split("&").Select(x => x.Split("=")).ToDictionary(x => x[0], x => x[1]);
        }
        catch (Exception) { return; };

        if (query.ContainsKey("ShowImage"))
        {
          IsImage = true;
          MediaSource = query["ShowImage"];
          isHidden = false;
        }
        else if (query.ContainsKey("ShowVideo"))
        {
          IsVideo = true;
          MediaSource = query["ShowVideo"];
          isHidden = false;
        }

        await Show();
      }
      return;
    }

    public async Task Show()
    {
      if (IsImage || IsVideo)
      {
        ChildContent = null;
        StateHasChanged();


      }
    }
  }
}
