using products.Domain.Itens.Entities;

namespace products.Domain.Carts.Commands;
public record UpdateCartCommand(int Id, List<Item> Itens, int Quantity, decimal Total);