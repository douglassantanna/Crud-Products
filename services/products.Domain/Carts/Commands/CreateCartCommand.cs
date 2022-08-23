using MediatR;
using products.Domain.Itens.Entities;
using products.Domain.Shared;

namespace products.Domain.Carts.Commands;
public record CreateCartCommand(int UserID, int ItemId, int Quantity) : IRequest<NotificationResult>;