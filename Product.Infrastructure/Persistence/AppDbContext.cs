using Microsoft.EntityFrameworkCore;

namespace Product.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Domain.Entities.Product> Products => Set<Domain.Entities.Product>();
}