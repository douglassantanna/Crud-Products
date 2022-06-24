using MediatR;
using products.Domain.Itens.Commands;
using products.Domain.Itens.Interfaces;
using products.Domain.Shared;

namespace products.Domain.Itens.Handlers
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, NotificationResult>
    {
        private readonly IItemRepository _repository;

        public UpdateItemCommandHandler(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<NotificationResult> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = _repository.GetById(request.Id);
            if(item == null) return new NotificationResult("Item invalido", false);
            item.UpdateName(request.Name);
            item.UpdatePrice(request.Price);
            item.UpdatedAt();
            await _repository.UpdateAsync(item);
            return new NotificationResult("Item atualizado");
        }
    }
}