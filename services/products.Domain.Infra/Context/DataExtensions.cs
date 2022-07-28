using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using products.Domain.Customers.Interfaces;
using products.Domain.Infra.Omie;
using products.Domain.Infra.Repositories.CustomerRepo;
using products.Domain.Infra.Repositories.ItemRepo;
using products.Domain.Infra.Repositories.OrderRepo;
using products.Domain.Itens.Interfaces;
using products.Domain.Omie;
using products.Domain.Orders.Interfaces;

namespace products.Domain.Infra.Context
{
    public static class DataExtensions
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("dshop")));
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("dshop"));
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOmieCustomer, OmieCustomerService>();
            return services;
        }
    }
}