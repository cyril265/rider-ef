using Entities;
using Microsoft.EntityFrameworkCore;

public class FooDbContext : DbContext
{
    public FooDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Person).Assembly);
    }

    public DbSet<Person> Person { get; init; } = null!;
}