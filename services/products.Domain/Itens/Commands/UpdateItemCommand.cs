using FluentValidation;
using MediatR;
using products.Domain.Shared;

namespace products.Domain.Itens.Commands;

public class UpdateItemCommand : IRequest<NotificationResult>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
}
public class UpdateItemValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Name).NotNull().NotEmpty().Length(2, 100).WithMessage("Um nome para o item deve ser fornecido");
        RuleFor(x => x.Price).NotNull().NotEmpty().GreaterThan(0).WithMessage("Preço deve ser fornecido");
    }
}