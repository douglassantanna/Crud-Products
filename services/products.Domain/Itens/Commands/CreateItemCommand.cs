using FluentValidation;
using MediatR;
using products.Domain.Itens.Entities;
using products.Domain.Shared;

namespace products.Domain.Itens.Commands;

public record CreateItemCommand(string Name, decimal Price, Category Category) : IRequest<NotificationResult>;
public class CreateItemValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Name).NotNull().NotEmpty().Length(2, 100).WithMessage("Informe um nome.");
        RuleFor(x => x.Price).NotNull().NotEmpty().GreaterThan(0).WithMessage("Preço deve ser maior que 0");
        RuleFor(x => x.Category).NotNull().NotEmpty().WithMessage("Categoria deve ser especificada");
        // .ScalePrecision(10, 2).WithMessage("Formato invalido. Use por exemplo 100.90");
    }
}