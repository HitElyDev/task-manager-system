
using Microsoft.EntityFrameworkCore;
using HTask.Application.Interfaces;
using HTask.Application.Services;
using HTask.Domain.Interfaces;
using HTask.Infrastructure;
using HTask.Infrastructure.Data;
using HTask.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no fue encontrada en la configuración.");
}

builder.Services.AddDbContext<HTaskDbContext>(options =>
    options.UseSqlServer(connectionString));



builder.Services.AddDbContext<HTaskDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<IHTaskRepository, HTaskRepository>();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<IHTaskService, HTaskServices>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Habilitar Swagger en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
}

//  Manejo Global de Errores (para devolver 400, 404, 500)

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