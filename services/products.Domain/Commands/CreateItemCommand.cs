using MediatR;

namespace products.Domain.Commands;

public class CreateItemCommand : IRequest<string>
{
    public string Name { get; set; }
    public double Price { get; set; }
}
