using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PokerHandKata.Client.Poker;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Add services to the container.
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddHttpClient();
services.AddTransient(provider =>
{
	var httpClient = provider.GetRequiredService<HttpClient>();
	return PokerService.CreateServiceCall(httpClient);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
