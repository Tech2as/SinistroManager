using SinistroManager.Enums;
using SinistroManager.Models;

namespace SinistroManager.Services;

public interface IUserService
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User> CreateAsync(string name, string email, string passwordHash, UserRole role);
}
