using Cart.API.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
namespace Cart.API.Data
{
    public class CachedBasketRepository : IBasketRepository
    {
        private readonly IBasketRepository _repository;
        private readonly IDistributedCache _cache;
        public CachedBasketRepository(IBasketRepository repository,IDistributedCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<bool> AddItems( CartItemsDTO Items, string UserId)
        {
            await _repository.AddItems(Items, UserId);
            return true;
        }

        public async Task<bool> CreateUserBasket(Guid UserId, string UserName)
        {
            await _repository.CreateUserBasket(UserId, UserName);
            return true;
        }

        public async Task<bool> DeleteBasketItem(Guid UserId)
        {
            await _repository.DeleteBasketItem(UserId);
            await _cache.RemoveAsync(UserId.ToString());
            return true;

        }

       

        public async Task<ShoppingCart> GetBasket(Guid UserId, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await _cache.GetStringAsync(UserId.ToString(), cancellationToken);
            if(!string.IsNullOrEmpty(cachedBasket))
            {
               return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }
            var basket = await _repository.GetBasket(UserId, cancellationToken);
            await _cache.SetStringAsync(UserId.ToString(), JsonSerializer.Serialize(basket),cancellationToken);
            return basket;
        }

        
    }
}
