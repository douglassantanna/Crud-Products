using Microsoft.EntityFrameworkCore;
using products.Domain.Customers.Entities;
using products.Domain.Itens.Entities;

namespace products.Domain.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Item> Itens { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().Property(p => p.Price).HasColumnType("decimal(10,2)");
    }

}
