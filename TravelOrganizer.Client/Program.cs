using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TravelOrganizer.Client;
using TravelOrganizer.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// =======================================================
// CONFIGURACIÓN DEL HTTP CLIENT
// =======================================================
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7069/")
});


// =======================================================
// REGISTRO DE SERVICIOS PERSONALIZADOS
// =======================================================
builder.Services.AddScoped<TripApiService>();
builder.Services.AddScoped<ActivityApiService>();
builder.Services.AddScoped<ExpenseApiService>();

await builder.Build().RunAsync();
