using FluentValidation;
using MediatR;
using products.Domain.Shared;

namespace products.Domain.Itens.Commands;

public class DeleteItemCommand : IRequest<NotificationResult>
{
    public int Id { get; set; }

    public DeleteItemCommand(int id)
    {
        Id = id;
    }
}
public class DeleteItemValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("An id must be provided");
    }
}