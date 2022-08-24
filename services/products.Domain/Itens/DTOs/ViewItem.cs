using System.Linq.Expressions;
using products.Domain.Itens.Entities;

namespace products.Domain.Itens.DTOs;

public class ViewItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}
public static class ViewItemExtension
{
    public static Expression<Func<Item, ViewItem>> ToView() => x => new ViewItem
    {
        Id = x.Id,
        Name = x.Name,
        Price = x.Price
    };
}
