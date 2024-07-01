using BuildingBlocks.CQRS;
using Cart.API.Data;
using Cart.API.DTOs;
using Cart.API.Models;

namespace Cart.API.Basket.GetBasket
{
    public record GetBasketQuery(Guid UserId): IQuery<GetBasketResult>;
    public record GetBasketResult(CartResponseDTO CartDTO);
    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        private readonly IBasketRepository _repository;
        public GetBasketQueryHandler(IBasketRepository repository)
        {
            _repository = repository;
        }
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {

            var cart = await _repository.GetBasket(query.UserId, cancellationToken);
            var cartDTO = new CartResponseDTO { CartId=cart.CartId,UserId=cart.UserId,UserName=cart.UserName,TotalPrice=cart.TotalPrice};
            var cartItemDTOs = cart.Items.Select(x => x.ToCartItemsDTO()).ToList();
            cartDTO.CartItemDTO=cartItemDTOs;
            return new GetBasketResult(cartDTO);
        }
    }
}
