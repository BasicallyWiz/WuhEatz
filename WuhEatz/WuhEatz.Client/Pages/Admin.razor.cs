using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using WizWebComponents.Services;
using WuhEatz.Shared.DenpaDB.Models;

namespace WuhEatz.Client.Pages
{
  partial class Admin
  {
    [Inject] IJSRuntime? JS { get; set; }
    [Inject] NavigationManager? Nav { get; set; }
    [Inject] HttpClient? Http { get; set; }
    bool verified = false;
    List<UserProfile> users { get; set; } = new();

    ElementReference UsersList { get; set; }

    int currentPage = 0;
    int itemsPerPage = 50;

    protected override async Task OnParametersSetAsync()
    {
      await GetUsers(0, 50);

      base.OnParametersSet();
    }

    public async Task IncrementPage()
    {
      await GetUsers(currentPage + 1, itemsPerPage);
    }
    public async Task DecrementPage()
    {
      await GetUsers(currentPage - 1, itemsPerPage);
    }

    public async Task GetUsers(int page, int count)
    {
      currentPage = page;
      itemsPerPage = count;

      var result = await Http?.GetAsync($"{Nav?.BaseUri}api/Users/list/{page}?itemsPerPage={count}")!;
      if (!result.IsSuccessStatusCode)
      {
        users = new();
        return;
      }
      else
      {
        users = await result.Content.ReadFromJsonAsync<List<UserProfile>>() ?? new();
      }

      StateHasChanged();
    }
  }
}
