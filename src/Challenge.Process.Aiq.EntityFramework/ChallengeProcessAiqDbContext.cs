using Challenge.Process.Aiq.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Process.Aiq.EntityFramework;

public class ChallengeProcessAiqDbContext(DbContextOptions options): DbContext(options)
{
    public DbSet<Product> Product { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<FavoriteProduct> FavoriteProduct { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChallengeProcessAiqDbContext).Assembly);
    }
}