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

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7064") });
builder.Services.AddHttpClient(Constants.systemHttpClient, client =>
{
    client.BaseAddress = new Uri("https://localhost:7064");
    Console.WriteLine(client.BaseAddress);
});


//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IHttpClientUtil,HttpClientUtil>();
builder.Services.AddScoped<IAppLocalStorageService, BlazoredLocalServiceManager>();
builder.Services.AddScoped<IHttpClientUtil, HttpClientUtil>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IDummyService, DummyManager>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();



await builder.Build().RunAsync();
