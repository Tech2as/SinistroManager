using Microsoft.EntityFrameworkCore;
using SinistroManager.Models;

namespace SinistroManager.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
     
    // Usuarios do sistema, incluindo oficinas e reguladores  
    public DbSet<User> Users => Set<User>();
    public DbSet<OficinaProfile> Oficinas => Set<OficinaProfile>();
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

        modelBuilder.Entity<OficinaProfile>(e =>
        {
            e.HasKey(o => o.Id);

            e.HasOne<User>()
            .WithOne()
            .HasForeignKey<OficinaProfile>(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            e.Property(o => o.Cnpj)
            .IsRequired()
            .HasMaxLength(14);
            e.HasIndex(o => o.Cnpj).IsUnique();

            e.Property(o => o.Address)
            .IsRequired()
            .HasMaxLength(300);

            e.Property(o => o.AddressNumber)
            .IsRequired()
            .HasMaxLength(10);

            e.Property(o => o.Cep)
            .IsRequired()
            .HasMaxLength(8);

            e.Property(o => o.City)
            .IsRequired()
            .HasMaxLength(100);

            e.Property(o => o.State)
            .IsRequired()
            .HasMaxLength(2);

            e.Property(o => o.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);

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

        modelBuilder.Entity<Apolice>(e =>
        {
            e.HasKey(a => a.Id);

             e.HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            e.Property(a => a.NumeroApolice)
            .IsRequired()
            .HasMaxLength(50);


            e.Property(a => a.ValorCobertura).HasColumnType("decimal(18,2)");
            e.Property(a => a.DataHistorico).IsRequired();
        });
    }
}
