using Blazor.WhyDidYouRender.Configuration;
using Blazor.WhyDidYouRender.Extensions;

using Blazored.LocalStorage;

using UniDecoderBlazorServer.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddSingleton<UniDecoderBlazorServer.Services.UnidecoderService>();

// Add WhyDidYouRender - works automatically!
builder.Services.AddWhyDidYouRender(config =>
{
    config.Enabled = true;
    config.Verbosity = TrackingVerbosity.Verbose;
    config.Output = TrackingOutput.Both; // Server console AND browser console
    config.TrackParameterChanges = true;
    config.TrackPerformance = true;
    config.EnableStateTracking = true; // Track field-level changes
    config.AutoTrackSimpleTypes = true; // Auto-track strings, ints, etc.
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Initialize WhyDidYouRender services -- errors when I enable this, but seems to work fine without.
// app.Services.InitializeSSRServices();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
