using Blazored.SessionStorage;
using MatBlazor;
using Microsoft.FluentUI.AspNetCore.Components;
using WebBolt.Client.Pages;
using WebBolt.Client.Services;
using WebBolt.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddMatBlazor();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<ClientMembershipService>();
builder.Services.AddScoped<RegisterUserSevices>();
builder.Services.AddScoped<EntryService>();
builder.Services.AddScoped<GymService>();
builder.Services.AddScoped<MembershipService>();
builder.Services.AddFluentUIComponents();

builder.Services.AddMatToaster(config =>
{
    config.Position = MatToastPosition.TopRight;
    config.PreventDuplicates = true;
    config.NewestOnTop = true;
    config.ShowCloseButton = true;
    config.MaximumOpacity = 95;
    config.VisibleStateDuration = 3000;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();
