
using Cart.API.Data;

namespace Cart.API.Basket.RemoveItem
{
    public record RemoveItemCommand(Guid UserId,Guid ProductId): ICommand<RemoveItemResult>;
    public record RemoveItemResult(bool IsSuccess);
    public class RemoveItemHandler : ICommandHandler<RemoveItemCommand, RemoveItemResult>
    {
        private readonly IBasketRepository _repository;
        public RemoveItemHandler(IBasketRepository repository)
        {
            _repository = repository;
        }
        public async Task<RemoveItemResult> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.RemoveBasketItem(request.UserId, request.ProductId);
            return new RemoveItemResult(result);
        }
    }
}
