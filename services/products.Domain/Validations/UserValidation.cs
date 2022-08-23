using FluentValidation;
using products.Domain.Auth.Commands;

public class UserValidation : AbstractValidator<CreateUserCommand>
{
    public UserValidation()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName cant be empty.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password cant be empty.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email cant be empty.");
    }
}