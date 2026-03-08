using Microsoft.EntityFrameworkCore;
using SinistroManager.Models;

namespace SinistroManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Sinistro> Sinistros => Set<Sinistro>();

      public DbSet<Apolice> Apolices => Set<Apolice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);

            e.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(150);

            e.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Role).HasConversion<string>();
        });

        modelBuilder.Entity<Sinistro>(e =>
        {
            e.HasKey(s => s.Id);

            // id da oficina
            e.HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.OficinaId)
            .OnDelete(DeleteBehavior.Restrict);

            // id do regulador
            e.HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.ReguladorId)
            .OnDelete(DeleteBehavior.Restrict);

            e.Property(s => s.Chassi)
            .IsRequired()
            .HasMaxLength(17);
            
            e.Property(s => s.ValorReparo).HasColumnType("decimal(18,2)");
            e.Property(s => s.Salvado).IsRequired();
            e.Property(s => s.Status).HasConversion<int>();
            e.Property(s => s.DataSinistro).IsRequired();

        });
    }
}
