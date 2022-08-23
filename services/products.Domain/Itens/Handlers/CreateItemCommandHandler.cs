using MediatR;
using products.Domain.Itens.Commands;
using products.Domain.Itens.Entities;
using products.Domain.Itens.Contracts;
using products.Domain.Shared;

namespace products.Domain.Itens.Handlers
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, NotificationResult>
    {
        private readonly IItemRepository _repository;

        public CreateItemCommandHandler(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<NotificationResult> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            if (request is null) return new NotificationResult("request nao pode ser nulo", false);
            var item = new Item(request.Name, request.Price);
            await _repository.CreateAsync(item);
            return new NotificationResult("Item criado");
        }
    }
}