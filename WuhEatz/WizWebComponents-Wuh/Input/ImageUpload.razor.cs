using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace WizWebComponents.Input
{
  partial class ImageUpload
  {
    [Parameter] public string Accept { get; set; } = "";
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? SplatStuff { get; set; }
    [Parameter] public bool AcceptMultiple { get; set; } = false;
    List<IBrowserFile> BrowserFiles { get; set; } = new List<IBrowserFile>();

    private ElementReference previewImageElem;
    async Task Input_OnChange(InputFileChangeEventArgs args)
    {
      if (args.FileCount > 1)
      {
        BrowserFiles = args.GetMultipleFiles().ToList();
      }
    }
  }
}

//  DO NOT REMOVE
//(My cat typed this)
/*
09999999999=--------p0-
000000000000000000000000000000000000000\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                 lkoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
*/