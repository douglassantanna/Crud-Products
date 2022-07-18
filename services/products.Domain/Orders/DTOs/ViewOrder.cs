using System.Linq.Expressions;
using products.Domain.Customers.Entities;
using products.Domain.Itens.Entities;
using products.Domain.Orders.Entities;

namespace products.Domain.Itens.DTOs;

public class ViewOrder
{
    public int Id { get; set; }
    public Customer? Customer { get; set; }
    public List<Item>? Itens { get; set; }
}
public static class ViewOrderExtension
{
    public static Expression<Func<Order, ViewOrder>> ToView() => x => new ViewOrder
    {
        Id = x.Id,
        Customer = x.Customer,
        Itens = x.Itens
    };
}
