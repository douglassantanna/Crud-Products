using api.Product.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Product.Infra
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }

    }
}