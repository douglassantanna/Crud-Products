using MediatR;
using products.Domain.Itens.Commands;
using products.Domain.Itens.Interfaces;
using products.Domain.Shared;

namespace products.Domain.Itens.Handlers
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, NotificationResult>
    {
        private readonly IItemRepository _repository;

        public DeleteItemCommandHandler(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<NotificationResult> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var item = _repository.GetById(request.Id);
            if(item == null) return new NotificationResult("Item not found", false);
            await _repository.DeleteAsync(item);
            return new NotificationResult("Item deleted");
        }
    }
}