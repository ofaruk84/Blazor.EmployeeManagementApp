using Blazored.LocalStorage;
using Client.BlazorUI;
using Client.Lib.Services.Abstract;
using Client.Lib.Services.Concrete;
using Client.Lib.Services.Dummy;
using Client.Lib.Utilities;
using Client.Lib.Utilities.Authentication;
using Client.Lib.Utilities.Http;
using Client.Lib.Utilities.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(Constants.systemHttpClient, client =>
{
    client.BaseAddress = new Uri("https://localhost:7064");
});

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IHttpClientUtil,HttpClientUtil>();
builder.Services.AddScoped<IAppLocalStorageService, BlazoredLocalServiceManager>();
builder.Services.AddScoped<IHttpClientUtil, HttpClientUtil>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IDummyService, DummyManager>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();

builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<SfDialogService>();

builder.Services.AddScoped<DialogService>();

await builder.Build().RunAsync();
