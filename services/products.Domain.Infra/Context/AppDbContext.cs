using Microsoft.EntityFrameworkCore;
using products.Domain.Customers.Entities;
using products.Domain.Itens.Entities;
using products.Domain.Orders.Entities;

namespace products.Domain.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Item> Itens { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<EnderecoEntrega>? EnderecosEntrega { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
        .HasOne<Order>()
        .WithMany(o => o.Itens)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EnderecoEntrega>()
        .HasOne<Customer>()
        .WithMany(x => x.EnderecoEntrega)
        .OnDelete(DeleteBehavior.Cascade);
    }
    //api como projeto de inicializacao e rodar migration no infra
}
