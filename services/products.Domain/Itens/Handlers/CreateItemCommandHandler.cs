using MediatR;
using products.Domain.Itens.Commands;
using products.Domain.Itens.Entities;
using products.Domain.Itens.Interfaces;
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
            var item = new Item(request.Name, request.Price);
            if(string.IsNullOrEmpty(item.Name)) return new NotificationResult("Nome não pode ser vázio.", false);
            if(item.Price == 0) return new NotificationResult("Preço deve ser maior que 0.", false);
            await _repository.CreateAsync(item);
            return new NotificationResult("Item criado");
        }
    }
}