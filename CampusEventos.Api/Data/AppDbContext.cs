using Microsoft.EntityFrameworkCore;
using CampusEventos.Api.Models;

namespace CampusEventos.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Evento> Eventos => Set<Evento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapeamento da entidade Categoria
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("TB_CATEGORIAS");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nome).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Descricao).HasMaxLength(250);
            entity.Property(c => c.Ativo).HasDefaultValue(true);

            // Relacionamento 1:N
            entity.HasMany(c => c.Eventos)
                  .WithOne(e => e.Categoria)
                  .HasForeignKey(e => e.CategoriaId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Mapeamento da entidade Evento
        modelBuilder.Entity<Evento>(entity =>
        {
            entity.ToTable("TB_EVENTOS");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Descricao).HasMaxLength(500);
            entity.Property(e => e.DataHora).IsRequired();
            entity.Property(e => e.Local).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CapacidadeMaxima).IsRequired();
            entity.Property(e => e.Preco).HasColumnType("NUMBER(10,2)");
        });

        // Carga inicial de dados (Seed) para testes rápidos
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria
            {
                Id = 1,
                Nome = "Workshop",
                Descricao = "Oficinas práticas e capacitações técnicas",
                Ativo = true
            },
            new Categoria
            {
                Id = 2,
                Nome = "Palestra",
                Descricao = "Apresentações com especialistas do mercado",
                Ativo = true
            },
            new Categoria
            {
                Id = 3,
                Nome = "Hackathon",
                Descricao = "Maratonas de desenvolvimento e inovação",
                Ativo = true
            }
        );

        modelBuilder.Entity<Evento>().HasData(
            new Evento
            {
                Id = 1,
                Titulo = "Imersão em C# e .NET 10",
                Descricao = "Workshop focado em novidades do C# e boas práticas com EF Core",
                DataHora = new DateTime(2026, 10, 15, 19, 0, 0),
                Local = "Laboratório 504 - Campus Paulista",
                CapacidadeMaxima = 40,
                Preco = 0.00m,
                CategoriaId = 1
            },
            new Evento
            {
                Id = 2,
                Titulo = "Inteligência Artificial Generativa no Mercado",
                Descricao = "Palestra sobre o impacto da IA no desenvolvimento corporativo",
                DataHora = new DateTime(2026, 10, 20, 20, 0, 0),
                Local = "Auditório FIAP",
                CapacidadeMaxima = 150,
                Preco = 0.00m,
                CategoriaId = 2
            }
        );
    }
}
