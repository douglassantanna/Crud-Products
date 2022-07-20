using FluentValidation;
using MediatR;
using products.Domain.Shared;

namespace products.Domain.Customers.Commands;

public class DeleteCustomerCommand : IRequest<NotificationResult>
{
    public DeleteCustomerCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
public class DeleteCustomerValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Um ID deve ser fornecido.");
    }
}
