# Development on the client
It just needs to be activated, I guess.

So after about a week of trying to get this fucking client to actually be a client, I finally found [this page](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes?view=aspnetcore-8.0)
that led me to learn that the client project is required to run components and login on the client side, however needs to be first activated
by making a proper client component.

## How to do the shit
If you create a new component or page, you must give it the `@rendermode` attribute set to either `InteractiveWebAssembly` or `InteractiveAuto`.

File structure is enforced, or the whole thing doesn't work:
- Pages must be in the `WuhEatz.Client.Pages` namespace
- Components must be in the root namespace: `WuhEatz.Client`

See examples for rendermodes [here](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes?view=aspnetcore-8.0#blazor-documentation-examples-for-blazor-web-apps)