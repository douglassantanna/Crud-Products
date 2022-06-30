using System.Linq.Expressions;
using products.Domain.Customers.Entities;

namespace products.Domain.Customers.DTOs;

public class ViewCustomer
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
}

public static class ViewCustomerExtension
{
    public static Expression<Func<Customer, ViewCustomer>> ToView() => x => new ViewCustomer
    {
        Id = x.Id,
        FullName = x.FullName,
        Email = x.Email,
        BirthDate = x.BirthDate
    };
}

