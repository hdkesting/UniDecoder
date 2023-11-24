using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<UniDecoderBlazorServer.Services.UnidecoderService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();

app.MapBlazorHub();

app.MapRazorComponents<UniDecoderBlazorServer.Pages.App>()
    .AddInteractiveServerRenderMode();

app.Run();
