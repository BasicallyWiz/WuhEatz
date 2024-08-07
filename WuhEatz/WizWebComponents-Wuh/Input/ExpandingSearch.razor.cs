/*
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using OtaKuma.Client.Data;
using System.Text.Json;

namespace OtaKuma.Client.Components.Input
{
  class searchResult
  {
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public string TypeQuery 
    { 
      get {
        switch (Type)
        {
          case "MA": return "Manga";
          case "AN": return "Anime";
          case "User": return "Profile";
          default: return "Unknown";
        }
      } 
    }
  }

  partial class ExpandingSearch
  {
    //[Inject] KumaClient? KumaClient { get; set; } Will probably be used in future for socket-searching
    [Inject] NavigationManager? Nav { get; set; }
    [Inject] KumaClient? Client { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> SplatAttributes { get; set; } = new();
    HubConnection? hubConnection;

    string SearchTerm { get; set; } = string.Empty;
    List<searchResult> results = new();

    protected override async Task OnParametersSetAsync()
    {
      await base.OnParametersSetAsync();
    }

    async Task SearchBar_OnFocusIn() {
     hubConnection = new HubConnectionBuilder()
        .WithUrl($"{Client!.BaseAddress}Find")
        .Build();

      await hubConnection.StartAsync();

      hubConnection.On<string>("Results", (results) =>
      {
        Console.WriteLine("Results: " + results);
        this.results = JsonSerializer.Deserialize<List<searchResult>>(results);
      });
    }
    async Task SearchBar_OnFocusOut() {
      await Task.Delay(100);
      results.Clear();
      if (hubConnection is not null)
      {
        try
        {
          await hubConnection.StopAsync();
          await hubConnection.DisposeAsync();
        } catch (Exception ex) {
          Console.WriteLine(ex);
        }
      }
    }
    async Task SearchBar_OnInput(ChangeEventArgs e)
    { 
      SearchTerm = e.Value?.ToString() ?? string.Empty;
      if (SearchTerm == string.Empty) return;
      await hubConnection?.SendAsync("Find", SearchTerm)!;
    }
  }
}
*/