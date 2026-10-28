using Microsoft.EntityFrameworkCore;
using TechStore.Models;

namespace TechStore.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Categoria> Categorias { get; set; } = null!;
    public DbSet<Produto> Produtos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nome).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Descricao).HasMaxLength(500);
            entity.HasIndex(c => c.Nome).IsUnique();
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Nome).IsRequired().HasMaxLength(150);
            entity.Property(p => p.Descricao).HasMaxLength(1000);
            entity.Property(p => p.Preco).HasColumnType("decimal(18,2)");
            entity.Property(p => p.QuantidadeEstoque).IsRequired();

            entity.HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
