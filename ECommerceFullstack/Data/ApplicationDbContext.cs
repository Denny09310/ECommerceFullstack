using Microsoft.EntityFrameworkCore;

namespace ECommerceFullstack.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasQueryFilter(p => p.DeletedAt == null);

        modelBuilder.Entity<OrderProduct>()
            .HasQueryFilter(p => p.Product.DeletedAt == null);

        modelBuilder.Entity<User>()
            .Property(p => p.Role)
            .HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }
}