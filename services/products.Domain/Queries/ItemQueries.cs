using System.Linq.Expressions;
using products.Domain.Entities;

namespace products.Domain.Queries;
public static class ItemQueries
{
    public static Expression<Func<Item, bool>> GetAll(string name)
    {
        return x => x.Name == name;
    }
}