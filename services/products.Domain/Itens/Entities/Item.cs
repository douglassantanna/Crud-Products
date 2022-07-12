using products.Domain.Itens.Exceptions;
using products.Domain.Shared;

namespace products.Domain.Itens.Entities;

public class Item : Entity
{
    public Item(string name, double price)
    {
        Name = name;
        if(string.IsNullOrEmpty(name)) throw new ItemException("Nome obrigatório");
        Price = price;
        if(price <= 0) throw new ItemException("Preço do item deve ser maior que 0");
        CreatedAt = DateTime.UtcNow;
    }
    protected Item() { }

    public string Name { get; private set; }
    public double Price { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedWhen { get; private set; }
    public void UpdateName(string name) => Name = name;
    public void UpdatePrice(double price) => Price = price;
    public void UpdatedAt() => UpdatedWhen = DateTime.UtcNow;
}
