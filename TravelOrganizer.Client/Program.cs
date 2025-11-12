using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TravelOrganizer.Client;
using TravelOrganizer.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ============================
// CONFIGURAR HTTP CLIENT
// ============================
// Detecta si estás en Render o en local automáticamente
string baseUrl;

#if DEBUG
baseUrl = "https://localhost:7069/"; // para Visual Studio local
#else
baseUrl = builder.HostEnvironment.BaseAddress; // para Render (usa la URL pública)
#endif

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(baseUrl)
});

// ============================
// REGISTRAR SERVICIOS
// ============================
builder.Services.AddScoped<TripApiService>();
builder.Services.AddScoped<ActivityApiService>();
builder.Services.AddScoped<ExpenseApiService>();

await builder.Build().RunAsync();
