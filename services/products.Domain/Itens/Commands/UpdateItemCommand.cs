using FluentValidation;
using MediatR;
using products.Domain.Shared;

namespace products.Domain.Itens.Commands;

public class UpdateItemCommand : IRequest<NotificationResult>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
}
public class UpdateItemValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemValidator()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Name).NotNull().NotEmpty().Length(2, 100).WithMessage("A name must be provided");
        RuleFor(x => x.Price).NotNull().NotEmpty().GreaterThan(0).WithMessage("Please speciy a price");
    }
}