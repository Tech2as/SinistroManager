using SinistroManager.Enums;
using SinistroManager.Models;

namespace SinistroManager.Services;

public interface ISinistroService
{
    Task<List<Sinistro>> GetAllAsync();
    Task<Sinistro?> GetByIdAsync(Guid id);
    Task<Sinistro> CreateAsync(Guid oficinaId, Guid reguladorId, string chassi, double valorReparo, bool salvado, SinistroStatus status);
    Task AprovarAsync(Guid sinistroId, UserRole callerRole);
}
