using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using products.Domain.Auth.Contracts;
using products.Domain.Carts.Contracts;
using products.Domain.Customers.Contracts;
using products.Domain.Infra.Omie;
using products.Domain.Infra.Repositories;
using products.Domain.Itens.Contracts;
using products.Domain.Omie.OmieCustomers;
using products.Domain.Orders.Contracts;

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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            return services;
        }
    }
}