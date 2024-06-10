using BuildingBlocks.CQRS;
using Cart.API.Data;
using Cart.API.Models;

namespace Cart.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName): IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);
    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        private readonly IBasketRepository _repository;
        public GetBasketQueryHandler(IBasketRepository repository)
        {
            _repository = repository;
        }
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetBasket(query.UserName);
            return new GetBasketResult(cart);
        }
    }
}
