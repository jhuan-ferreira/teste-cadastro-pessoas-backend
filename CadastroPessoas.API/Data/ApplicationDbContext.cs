using Microsoft.EntityFrameworkCore;
using Pessoas.API.Models;

namespace Pessoas.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pessoa>()
            .HasIndex(p => p.Cpf)
            .IsUnique();
    }
}