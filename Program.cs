using Microsoft.EntityFrameworkCore;
using SinistroManager.Data;
using SinistroManager.Enums;
using SinistroManager.Models;
using SinistroManager.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));


var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

// Users

// Sinistros


app.Run();

record CreateUserRequest(string Name, string Email, string PasswordHash, int Role);
record CreateSinistroRequest(Guid OficinaId, Guid ReguladorId, string Chassi, double ValorReparo, bool Salvado, int? Status);
record AprovarSinistroRequest(int CallerRole);
