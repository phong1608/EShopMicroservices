using Cart.API.DTOs;

namespace Cart.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(Guid UserId,CancellationToken cancellationToken=default!);
        Task<bool> DeleteBasketItem(Guid UserId);
        Task<bool> CreateUserBasket(Guid UserId,string UserName );
        Task<bool> AddItems(CartItemsDTO Items,string UserId);
    }
}
