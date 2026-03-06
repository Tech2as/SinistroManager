using Microsoft.EntityFrameworkCore;
using SinistroManager.Data;
using SinistroManager.Enums;
using SinistroManager.Models;
using SinistroManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISinistroService, SinistroService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

// Users
app.MapGet("/users", (IUserService svc) => svc.GetAllAsync());
app.MapGet("/users/{id:guid}", async (Guid id, IUserService svc) =>
    await svc.GetByIdAsync(id) is { } u ? Results.Ok(u) : Results.NotFound());

app.MapPost("/users", async (CreateUserRequest req, IUserService svc) =>
{
    try
    {
        var user = await svc.CreateAsync(req.Name, req.Email, req.PasswordHash, (UserRole)req.Role);
        return Results.Created($"/users/{user.Id}", user);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// Sinistros
app.MapGet("/sinistros", (ISinistroService svc) => svc.GetAllAsync());
app.MapGet("/sinistros/{id:guid}", async (Guid id, ISinistroService svc) =>
    await svc.GetByIdAsync(id) is { } s ? Results.Ok(s) : Results.NotFound());

app.MapPost("/sinistros", async (CreateSinistroRequest req, ISinistroService svc) =>
{
    var status = (SinistroStatus)(req.Status ?? (int)SinistroStatus.EmAnalise);
    var sinistro = await svc.CreateAsync(req.OficinaId, req.ReguladorId, req.Chassi, req.ValorReparo, req.Salvado, status);
    return Results.Created($"/sinistros/{sinistro.Id}", sinistro);
});

app.MapPost("/sinistros/{id:guid}/aprovar", async (Guid id, AprovarSinistroRequest req, ISinistroService svc) =>
{
    try
    {
        await svc.AprovarAsync(id, (UserRole)req.CallerRole);
        return Results.Ok();
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (UnauthorizedAccessException)
    {
        return Results.Forbid();
    }
});

app.Run();

record CreateUserRequest(string Name, string Email, string PasswordHash, int Role);
record CreateSinistroRequest(Guid OficinaId, Guid ReguladorId, string Chassi, double ValorReparo, bool Salvado, int? Status);
record AprovarSinistroRequest(int CallerRole);
