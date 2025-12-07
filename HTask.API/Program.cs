
using Microsoft.EntityFrameworkCore;
using HTask.Application.Interfaces;
using HTask.Application.Services;
using HTask.Domain.Interfaces;
using HTask.Infrastructure;
using HTask.Infrastructure.Data;
using HTask.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// =====================================================================
// 1. CONFIGURACIÓN DE SERVICIOS
// =====================================================================

// 1.1. Configuración de EF Core y la Conexión a la BD
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Server=(DESKTOP-T485LKV)\\mssqllocaldb;Database=TaskManagerDb;Persist Security Info=True;User ID=sa;Password=123;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True";

builder.Services.AddDbContext<HTaskDbContext>(options =>
    options.UseSqlServer(connectionString));

// 1.2. Registro de Abstracciones e Implementaciones (DI - Inversión de Dependencia)
// Se registra la cadena de dependencia completa de abajo hacia arriba (DIP).

// Infraestructura: Repositorio (implementación concreta del contrato Domain)
builder.Services.AddScoped<IHTaskRepository, HTaskRepository>();

// Infraestructura: UoW (implementación concreta del contrato Domain)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Aplicación: Servicios de Lógica de Negocio (implementación concreta del contrato Application)
builder.Services.AddScoped<IHTaskService, HTaskServices>();

// 1.3. Controladores y Swagger (Documentación)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// =====================================================================
// 2. CONSTRUCCIÓN Y CONFIGURACIÓN DEL PIPELINE HTTP
// =====================================================================

var app = builder.Build();

// 2.1. Aplicar Migraciones al Iniciar

// ApplyMigrations(app); 

// 2.2. Configuración del HTTP Request Pipeline (Middleware)

if (app.Environment.IsDevelopment())
{
    // Habilitar Swagger en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 2.3. Manejo Global de Errores (para devolver 400, 404, 500)

// app.UseMiddleware<GlobalExceptionHandlerMiddleware>(); 

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


// Para  migraciones descomentar
/*
void ApplyMigrations(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
        db.Database.Migrate();
    }
}
*/