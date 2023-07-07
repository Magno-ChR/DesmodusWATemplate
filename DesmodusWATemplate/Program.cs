global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
using DesmodusWATemplate;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DesmodusWATemplate.Helpers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
//services.AddHttpContextAccessor();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IToast, ToastHelper>();
builder.Services.AddTransient<ToastHelper>();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();
//builder.Services.AddScoped<IMostrarMensajes, MostrarMensajes>();