using System.Linq.Expressions;
using products.Domain.Itens.Entities;

namespace products.Domain.Itens.Queries;
public static class ItemQueries
{
    public static Expression<Func<Item, bool>> GetAll(string name)
    {
        return x => x.Name == name;
    }
}