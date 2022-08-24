namespace products.Domain.Carts.Commands;
public record UpdateCartCommand(int Id, int ItemId, int Quantity);