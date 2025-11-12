using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TravelOrganizer.Data;
using TravelOrganizer.Dtos;
using TravelOrganizer.Repositories;
using TravelOrganizer.Services;
using TravelOrganizer.Validators;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// CONFIGURACIÓN DE SERVICIOS E INYECCIÓN DE DEPENDENCIAS
// ============================================================

// Controladores + vistas y RazorPages (necesario para Blazor WebAssembly)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Swagger (solo visible si es entorno de desarrollo)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------------------------------------------------
// CONFIGURACIÓN DE ENTITY FRAMEWORK CORE CON SQLITE
// ------------------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// ------------------------------------------------------------
// REGISTRO DE CAPAS: REPOSITORIOS, SERVICIOS Y VALIDADORES
// ------------------------------------------------------------
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IValidator<TripPostDto>, TripPostValidator>();

builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IValidator<ActivityPostDto>, ActivityPostValidator>();

builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IValidator<ExpensePostDto>, ExpensePostValidator>();

// ------------------------------------------------------------
// CONFIGURACIÓN DE CORS
// ------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ============================================================
// CONSTRUCCIÓN Y CONFIGURACIÓN DE LA APLICACIÓN
// ============================================================
var app = builder.Build();

// Activa Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ============================================================
// CONFIGURACIÓN DEL PIPELINE DE EJECUCIÓN
// ============================================================

// HTTPS opcional (Render no lo usa internamente, así que no es obligatorio)
app.UseHttpsRedirection();

// Archivos estáticos y cliente Blazor
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();
app.UseAuthorization();

// Mapear controladores y páginas
app.MapRazorPages();
app.MapControllers();

// Fallback al index.html del cliente Blazor
app.MapFallbackToFile("index.html");

app.Run();
