using Microsoft.EntityFrameworkCore;
using products.Domain.Auth.Entities;
using products.Domain.Carts.Entities;
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
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<CartHeader> CartHeaders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Item>()
        .HasOne<Order>()
        .WithMany(o => o.Itens)
        .OnDelete(DeleteBehavior.Cascade);

        mb.Entity<EnderecoEntrega>()
        .HasOne<Customer>()
        .WithMany(x => x.EnderecoEntrega)
        .OnDelete(DeleteBehavior.Cascade);
        mb.Entity<Customer>().HasKey(c => c.Id);
        mb.Entity<Customer>().Property(c => c.Bairro).HasMaxLength(50).IsRequired();
        mb.Entity<Customer>().Property(c => c.Cep).HasMaxLength(10).IsRequired();
        mb.Entity<Customer>().Property(c => c.Cidade).HasMaxLength(50).IsRequired();
        mb.Entity<Customer>().Property(c => c.Cnpj_cpf).HasMaxLength(15).IsRequired();
        mb.Entity<Customer>().Property(c => c.Codigo_cliente_integracao).HasMaxLength(50).IsRequired();
        mb.Entity<Customer>().Property(c => c.Codigo_cliente_omie).HasMaxLength(50).IsRequired();
        mb.Entity<Customer>().Property(c => c.Complemento).HasMaxLength(256).IsRequired();
        mb.Entity<Customer>().Property(c => c.Contato).HasMaxLength(50).IsRequired();
        mb.Entity<Customer>().Property(c => c.Contribuinte).HasMaxLength(50).IsRequired();
        mb.Entity<Customer>().Property(c => c.Email).HasMaxLength(256).IsRequired();
        mb.Entity<Customer>().Property(c => c.Nome_fantasia).HasMaxLength(256).IsRequired();
        mb.Entity<Customer>().Property(c => c.Razao_social).HasMaxLength(256).IsRequired();
        mb.Entity<Customer>().Property(c => c.Pessoa_fisica).HasMaxLength(50).IsRequired();
        mb.Entity<Customer>().Property(c => c.Telefone1_numero).HasMaxLength(20).IsRequired();

        mb.Entity<Order>().HasKey(c => c.Id);
        mb.Entity<Order>().Property(c => c.Receipt).HasMaxLength(100).IsRequired();

        mb.Entity<Address>().HasKey(c => c.Id);
        mb.Entity<Address>().Property(c => c.City).HasMaxLength(100).IsRequired();
        mb.Entity<Address>().Property(c => c.State).HasMaxLength(100).IsRequired();
        mb.Entity<Address>().Property(c => c.Street).HasMaxLength(256).IsRequired();
        mb.Entity<Address>().Property(c => c.ZipCode).HasMaxLength(20).IsRequired();



        mb.Entity<Category>().HasKey(c => c.Id);
        mb.Entity<Category>().Property(c => c.Name).HasMaxLength(50).IsRequired();
        mb.Entity<Category>().HasMany(i => i.Items).WithOne(c => c.Category).IsRequired().OnDelete(DeleteBehavior.Cascade);

        mb.Entity<Item>().HasKey(c => c.Id);
        mb.Entity<Item>().Property(c => c.Name).HasMaxLength(50).IsRequired();
        mb.Entity<Item>().Property(c => c.Price).HasPrecision(12, 2).IsRequired();

        mb.Entity<CartHeader>().HasKey(c => c.Id);
        mb.Entity<CartHeader>().Property(c => c.Voucer).HasMaxLength(100);

        mb.Entity<User>().HasKey(c => c.Id);
        mb.Entity<User>().Property(u => u.Email).HasMaxLength(256).IsRequired();
        mb.Entity<User>().Property(u => u.Password).HasColumnType("varchar").HasMaxLength(256).IsRequired();
        mb.Entity<User>().Property(u => u.UserName).HasMaxLength(100).IsRequired();


    }
    //api como projeto de inicializacao e rodar migration no infra
}
