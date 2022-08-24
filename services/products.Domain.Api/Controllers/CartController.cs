using MediatR;
using Microsoft.AspNetCore.Mvc;
using products.Domain.Carts.Contracts;
using products.Domain.Carts.DTOs;

namespace products.Domain.Api.Controllers;
[ApiController]
[Route("cart")]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepository;
    private readonly IMediator _mediator;

    public CartController(ICartRepository cartRepository, IMediator mediator)
    {
        _cartRepository = cartRepository;
        _mediator = mediator;
    }
    [HttpGet("getcart/{id}")]
    public async Task<ActionResult<CartDTO>> GetByUserId(int id)
    {
        var cartDTO = await _cartRepository.GetCartByUserID(id);
        if (cartDTO is null) return NotFound();
        return Ok(cartDTO);
    }

    [HttpPost("addcart")]
    public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO)
    {
        var cart = await _cartRepository.UpdateCart(cartDTO);
        if (cart is null) return NotFound();
        return Ok(cart);
    }

    [HttpPut("updatecart")]
    public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDTO)
    {
        var cart = await _cartRepository.UpdateCart(cartDTO);
        if (cart is null) return NotFound();
        return Ok(cart);
    }

    [HttpDelete("deletecart/{id}")]
    public async Task<ActionResult<CartDTO>> DeleteCart(int id)
    {
        var status = await _cartRepository.DeleteCartItem(id);
        if (!status) return BadRequest();
        return Ok(status);
    }
}