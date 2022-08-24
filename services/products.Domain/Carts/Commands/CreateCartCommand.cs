using MediatR;
using products.Domain.Carts.DTOs;
using products.Domain.Shared;

namespace products.Domain.Carts.Commands;
public record CreateCartCommand(CartHeaderDTO CartHeaderDto, IEnumerable<CartItemDTO> CartItems) : IRequest<NotificationResult>;