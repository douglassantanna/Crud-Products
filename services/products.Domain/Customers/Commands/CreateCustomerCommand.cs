using FluentValidation;
using MediatR;
using products.Domain.Shared;

namespace products.Domain.Customers.Commands;

public class CreateCustomerCommand : IRequest<NotificationResult>
{
    public CreateCustomerCommand(string? fullName, DateTime birthDate, string? email)
    {
        FullName = fullName;
        BirthDate = birthDate;
        Email = email;
    }

    public string? FullName { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? Email { get; private set; }
}
public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.FullName).NotNull().NotEmpty().Length(2, 100).WithMessage("Digite um nome entre 2 a 100 caracteres");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Um e-mail válido deve ser fornecido.");
        RuleFor(x => x.BirthDate).LessThan(DateTime.Now.Date).WithMessage("Data de nascimento invalida.");
        var date = DateTime.Now;
        var isUnderAge = new DateTime(date.Year - 18, date.Month, date.Day);
        RuleFor(x => x.BirthDate).LessThanOrEqualTo(isUnderAge).WithMessage("Necessário ter 18 anos ou mais.");

    }
}