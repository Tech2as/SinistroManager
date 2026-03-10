using Microsoft.EntityFrameworkCore;
using SinistroManager.Data;
using SinistroManager.Enums;
using SinistroManager.Models;

namespace SinistroManager.Services;

public class SinistroService
{
    private readonly AppDbContext _db;

    public SinistroService(AppDbContext db) => _db = db;

    public async Task<List<Sinistro>> GetAllAsync() =>
        await _db.Sinistros.OrderByDescending(s => s.DataSinistro).ToListAsync();

    public async Task<Sinistro?> GetByIdAsync(Guid id) => await _db.Sinistros.FindAsync(id);

    public async Task<Sinistro> CreateAsync(Guid oficinaId, Guid reguladorId, string chassi, decimal valorReparo, bool salvado, SinistroStatus status)
    {

        var sinistro = new Sinistro
        {
            Id = Guid.NewGuid(),
            OficinaId = oficinaId,
            ReguladorId = reguladorId,
            Chassi = chassi,
            ValorReparo = valorReparo,
            Salvado = salvado,
            Status = status,
            DataSinistro = DateTime.UtcNow
        };
        _db.Sinistros.Add(sinistro);
        await _db.SaveChangesAsync();
        return sinistro;
    }

    public async Task AprovarAsync(Guid sinistroId, UserRole callerRole)
    {
        var sinistro = await _db.Sinistros
        .Include(s => s.Apolice)
        .FirstOrDefaultAsync(s => s.Id == sinistroId)
        ?? throw new InvalidOperationException("Sinistro não encontrado.");

        if (sinistro.Status != SinistroStatus.EmAnalise)
            throw new InvalidOperationException("Somente sinistros em análise podem ser aprovados.");
            
        if (callerRole != UserRole.Regulador)
            throw new UnauthorizedAccessException("Apenas reguladores podem aprovar sinistros.");

        if (sinistro.ValorReparo > sinistro.Apolice.ValorCobertura)
            throw new InvalidOperationException("Valor ultrapassou o limite da apólice");

        sinistro.Status = SinistroStatus.Aprovado;
        await _db.SaveChangesAsync();
    }
}
