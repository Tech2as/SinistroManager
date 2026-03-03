using SinistroManager.Domain.Entities;
public interface iSinistroRepository
{
    Task<Sinistro?> GetByIdAsync(Guid id);
    Task AddAsync(Sinistro sinistro);
    Task SaveChangesAsync();
}