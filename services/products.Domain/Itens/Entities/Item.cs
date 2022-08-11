using products.Domain.Shared;

namespace products.Domain.Itens.Entities;

public class Item : Entity
{
    public Item(string name, double price)
    {
        if (string.IsNullOrEmpty(name)) throw new CustomException("Nome obrigatório");
        Name = name;
        if (price <= 0) throw new CustomException("Preço do item deve ser maior que 0");
        Price = price;
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
