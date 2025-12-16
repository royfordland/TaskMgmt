using Frontend.Components;
using Frontend.Services;
using Frontend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

// Register HttpClient so pages can inject HttpClient (e.g. Login/Register).
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7033") });

// Register token storage service (wraps localStorage via IJSRuntime)
builder.Services.AddScoped<IJsTokenStorage, JsTokenStorage>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();