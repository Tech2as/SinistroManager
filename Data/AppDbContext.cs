using Microsoft.EntityFrameworkCore;
using SinistroManager.Models;

namespace SinistroManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Sinistro> Sinistros => Set<Sinistro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.Name).IsRequired().HasMaxLength(150);
            e.Property(u => u.Email).IsRequired().HasMaxLength(200);
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Role).HasConversion<string>();
        });

        modelBuilder.Entity<Sinistro>(e =>
        {
            e.HasKey(s => s.Id);
            e.Property(s => s.Chassi).IsRequired();
            e.Property(s => s.Status).HasConversion<int>();
        });
    }
}
