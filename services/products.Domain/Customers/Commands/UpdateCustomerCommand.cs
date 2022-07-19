using FluentValidation;
using MediatR;
using products.Domain.Shared;

namespace products.Domain.Customers.Commands;

public record UpdateCustomerCommand(int Id, string Razao_social, string Email) : IRequest<NotificationResult>;
public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerValidator()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Razao_social).NotNull().NotEmpty().Length(2, 100).WithMessage("Digite um nome entre 2 a 100 caracteres");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Um e-mail válido deve ser fornecido.");
    }
}
