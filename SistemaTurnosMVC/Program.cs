using SistemaTurnosMVC.Models;
using SistemaTurnosMVC.Interface;
using SistemaTurnosMVC.Repository;
using SistemaTurnosMVC.Services;
using Biblioteca.Repository;
//using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Punto 3b tp 11

var CadenaDeConexion = builder.Configuration.GetConnectionString("SqliteConexion")!.ToString();
builder.Services.AddSingleton<string>(CadenaDeConexion);

// -----------------------------------------------------

builder.Services.AddHttpContextAccessor(); // Permite acceder a la sesión desde los servicios
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true;                // Seguridad de la cookie
    options.Cookie.IsEssential = true;             // Necesaria para el funcionamiento
});

// Registro de Repositorios (Requerido por el parcial)
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Registro del Servicio de Autenticación
builder.Services.AddScoped<IAuthenticationService, AutheticationService>();

// Configuración de Sesión (Necesaria para tu login)
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
