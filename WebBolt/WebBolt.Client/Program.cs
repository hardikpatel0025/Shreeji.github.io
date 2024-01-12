using Blazored.SessionStorage;
using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddMatBlazor();
await builder.Build().RunAsync();
