using MediatR;

namespace products.Domain.Commands;

public class DeleteItemCommand : IRequest<string>
{
    public int Id { get; set; }
}
