using Microsoft.EntityFrameworkCore;
using products.Domain.Entities;

namespace products.Domain.Infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Item> Itens { get; set; }
    }
}