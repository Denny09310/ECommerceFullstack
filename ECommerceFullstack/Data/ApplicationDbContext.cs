using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceFullstack.Data;

internal class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>()
            .HasQueryFilter(p => !p.DeletedAt.HasValue);

        builder.Entity<OrderProduct>()
            .HasQueryFilter(p => !p.Product.DeletedAt.HasValue);

        base.OnModelCreating(builder);
    }
}

internal static class IdentityExtensions
{
    internal static IEndpointConventionBuilder MapIdentityApi(this IEndpointRouteBuilder app)
    {
        return app.MapGroup("/api/identity")
                  .WithTags("Identity")
                  .MapIdentityApi<ApplicationUser>();
    }
}