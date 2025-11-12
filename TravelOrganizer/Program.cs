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

// Habilita los controladores (endpoints de la API)
builder.Services.AddControllers();

// Habilita Swagger (interfaz de pruebas de la API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------------------------------------------------
// CONFIGURACIÓN DE ENTITY FRAMEWORK CORE CON SQLITE
// ------------------------------------------------------------
// Usa la cadena de conexión definida en appsettings.json
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// ------------------------------------------------------------
// REGISTRO DE CAPAS: REPOSITORIOS, SERVICIOS Y VALIDADORES
// ------------------------------------------------------------

// ----- MÓDULO DE TRIPS -----
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IValidator<TripPostDto>, TripPostValidator>();

// ----- MÓDULO DE ACTIVITIES -----
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IValidator<ActivityPostDto>, ActivityPostValidator>();

// ----- MÓDULO DE EXPENSES -----
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IValidator<ExpensePostDto>, ExpensePostValidator>();

// ------------------------------------------------------------
// CONFIGURACIÓN DE CORS
// ------------------------------------------------------------
// Permite solicitudes desde el frontend Blazor (puerto 7161)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
            "https://localhost:7161" // Puerto del frontend Blazor
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// ============================================================
// CONSTRUCCIÓN Y CONFIGURACIÓN DE LA APLICACIÓN
// ============================================================

var app = builder.Build();

// Activa Swagger para probar la API desde el navegador
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirige automáticamente a HTTPS
app.UseHttpsRedirection();

// Habilita CORS (debe ir antes de MapControllers)
app.UseCors();

// Mapea todos los controladores (Trips, Activities, Expenses, etc.)
app.MapControllers();

// Ejecuta la aplicación
app.Run();
