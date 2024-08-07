using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace WizWebComponents.Collections
{
  partial class Gallery
  {
    [Parameter] public string[]? Uris { get; set; }
    [Parameter] public string Size { get; set; } = "15rem";
    [Parameter] public bool FlowHorizontal { get; set; } = true;

    string itemStyle { get
      {
        string style = "";
        if (FlowHorizontal)
        {
          style += "height: 100%; margin-right: 8px;";
        }
        else
        {
          style += "width: 100%; margin-bottom: 8px;";
        }
        return style;
      } }
    string galleryStyle { get
      {
        string style = "flex-direction: column;";

        if (FlowHorizontal)
        {
          style += $"overflow-x: auto; overflow-y: hidden; height: {Size}; white-space: nowrap; max-width: 100%; scrollbar-width: thin;";
        }
        else
        {
          style += $"overflow-x: hidden; overflow-y: auto; width: {Size}";
        }
        return style;
      } }
    //[Parameter] public bool UseURI { get; set } 
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? SplatStuff { get; set; }
  }
}
