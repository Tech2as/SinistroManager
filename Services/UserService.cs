using Microsoft.EntityFrameworkCore;
using SinistroManager.Data;
using SinistroManager.Enums;
using SinistroManager.Models;

namespace SinistroManager.Services;

public class UserService
{
    private readonly AppDbContext _db;

    public UserService(AppDbContext db) => _db = db;

    public async Task<List<User>> GetAllAsync() => await _db.Users.ToListAsync();

    public async Task<User?> GetByIdAsync(Guid id) => await _db.Users.FindAsync(id);

    public async Task<User> CreateAsync(string name, string email, string passwordHash, UserRole role)
    {
        if (await _db.Users.AnyAsync(u => u.Email == email))
            throw new InvalidOperationException("Já existe um utilizador com este email.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            PasswordHash = passwordHash,
            Role = role
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
}
