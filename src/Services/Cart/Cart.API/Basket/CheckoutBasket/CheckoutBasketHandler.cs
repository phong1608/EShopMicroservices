using BuildingBlocks.Messaging.Events;
using Cart.API.Data;
using Cart.API.DTOs;
using Cart.API.Exception;
using FluentValidation;
using Mapster;
using MassTransit;

namespace Cart.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDTO BasketCheckoutDto)
    : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator
        : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can't be null");
        }
    }
    public class CheckoutBasketHandler : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IPublishEndpoint _publisher;
        public CheckoutBasketHandler(IBasketRepository basketRepository, IPublishEndpoint publiser)
        {
            _basketRepository = basketRepository;
            _publisher = publiser;
        }
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasket(command.BasketCheckoutDto.CustomerId,cancellationToken);
            if (basket == null)
            {
                return new CheckoutBasketResult(false);
            }
            var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.UserName = basket.UserName!;
            eventMessage.CustomerId = command.BasketCheckoutDto.CustomerId;
            eventMessage.TotalPrice = basket.Items.Sum(item => item.Price);

            eventMessage.CartItems = basket.Items.Select(item => new CartItem
            {
                ProductId = item.ProductId,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList();
            await _publisher.Publish(eventMessage,cancellationToken);
            await _basketRepository.RemoveAllItem(basket.UserId!);
            return new CheckoutBasketResult(true);
        }
    }
}
