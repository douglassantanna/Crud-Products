using products.Domain.Shared;

namespace products.Domain.Itens.Entities;
public class Category : Entity
{
    public Category(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
    public ICollection<Item>? Items { get; set; }
}