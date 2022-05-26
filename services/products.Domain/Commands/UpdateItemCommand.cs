using MediatR;

namespace products.Domain.Commands;

public class UpdateItemCommand : IRequest<string>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}
