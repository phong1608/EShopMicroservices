using Cart.API.Data;
using FluentValidation;

namespace Cart.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(Guid UserId):ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);
    public class DeleteBasketCommandValidatore:AbstractValidator<DeleteBasketCommand>
    {

    }
    public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        private readonly IBasketRepository _repository;
        public DeleteBasketCommandHandler(IBasketRepository repository)
        {
             _repository = repository;
        }
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            var isSuccess = await _repository.DeleteAllBasketItem(command.UserId);
            return new DeleteBasketResult(isSuccess);
        }
    }
}
