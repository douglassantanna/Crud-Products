using FluentValidation;
using MediatR;
using products.Domain.Shared;

namespace products.Domain.Customers.Commands;

public class UpdateCustomerCommand : IRequest<NotificationResult>
{
    public int Id { get; set; }
    public string? FullName { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? Email { get; private set; }
}
public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.FullName).NotNull().NotEmpty().Length(2, 100).WithMessage("Digite um nome entre 2 a 100 caracteres");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Um e-mail válido deve ser fornecido.");
    }
}
