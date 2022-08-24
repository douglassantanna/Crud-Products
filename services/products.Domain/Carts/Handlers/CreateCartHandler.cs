using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Carts.Commands;
using products.Domain.Carts.Contracts;
using products.Domain.Itens.Contracts;
using products.Domain.Shared;

namespace products.Domain.Carts;
public class CreateCartHandler : IRequestHandler<CreateCartCommand, NotificationResult>
{
    private readonly IItemRepository _itemRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ILogger<CreateCartHandler> _logger;

    public CreateCartHandler(IItemRepository itemRepository, ICartRepository cartRepository, ILogger<CreateCartHandler> logger)
    {
        _itemRepository = itemRepository;
        _cartRepository = cartRepository;
        _logger = logger;
    }

    public async Task<NotificationResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(@"
        **********Process to create a cart have been initialized**********");

        if (request is null)
            return new("Request cant be null", false);
        // var cart = await _cartRepository.c(request.UserID, request.ItemId, request.Quantity);
        return new("Cart created.");
    }
}