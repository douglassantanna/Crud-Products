using FluentValidation;
using MediatR;
using products.Domain.Itens.Entities;
using products.Domain.Shared;

namespace products.Domain.Itens.Commands;

public record UpdateItemCommand(int Id, string Name, decimal Price, Category Category) : IRequest<NotificationResult>;
public class UpdateItemValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Name).NotNull().NotEmpty().Length(2, 100).WithMessage("Um nome para o item deve ser fornecido");
        RuleFor(x => x.Price).NotNull().NotEmpty().GreaterThan(0).WithMessage("Preço deve ser fornecido");
        RuleFor(x => x.Category).NotNull().NotEmpty().WithMessage("Categoria deve ser especificada");
    }
}