using FluentValidation;
using products.Domain.Carts.Entities;

namespace products.Domain.Validations;
public class CartValidation : AbstractValidator<Cart>
{
    public CartValidation()
    {

    }

}