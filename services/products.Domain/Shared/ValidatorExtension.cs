using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using products.Domain.Customers.Commands;

namespace products.Domain.Shared;
public static class ValidatorExtension
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreateCustomerValidator>());
            return services;
        }
}