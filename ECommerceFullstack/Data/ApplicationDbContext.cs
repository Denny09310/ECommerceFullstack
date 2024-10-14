using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasQueryFilter(p => p.DeletedAt == null);

        base.OnModelCreating(modelBuilder);
    }
}