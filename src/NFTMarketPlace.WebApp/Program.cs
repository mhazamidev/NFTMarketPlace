using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NFTMarketPlace.WebApp;
using NFTMarketPlace.MetaMask;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using NFTMarketPlace.WebApp.Base;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
//builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMetaMaskBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ClientAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, ClientAuthenticationStateProvider>();
builder.Services.AddScoped<MainLayoutCascadingValue>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri($"https://localhost:7035/api/"), Timeout = TimeSpan.FromMinutes(30) });

await builder.Build().RunAsync();
