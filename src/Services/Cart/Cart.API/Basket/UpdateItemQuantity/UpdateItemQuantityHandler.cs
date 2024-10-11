
using Cart.API.Data;

namespace Cart.API.Basket.UpdateItemQuantity
{
    public record UpdateItemQuantityCommand(Guid UserId,Guid ProductId,int Quantity):ICommand<UpdateItemQuantityResult>;
    public record UpdateItemQuantityResult(bool IsSuccess);
    public class UpdateItemQuantityHandler : ICommandHandler<UpdateItemQuantityCommand, UpdateItemQuantityResult>
    {
        private readonly IBasketRepository _repository;
        public UpdateItemQuantityHandler(IBasketRepository repository)
        {
            _repository = repository;
        }
        public async Task<UpdateItemQuantityResult> Handle(UpdateItemQuantityCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateItemQuantity(request.UserId, request.ProductId, request.Quantity);
            return new UpdateItemQuantityResult(true);
        }
    }
}
