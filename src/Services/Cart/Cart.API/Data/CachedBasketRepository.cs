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

        public async Task<bool> DeleteBasket(string UserName)
        {
            await _repository.DeleteBasket(UserName);
            await _cache.RemoveAsync(UserName);
            return true;

        }

        public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await _cache.GetStringAsync(UserName, cancellationToken);
            if(!string.IsNullOrEmpty(cachedBasket))
            {
               return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }
            var basket = await _repository.GetBasket(UserName, cancellationToken);
            await _cache.SetStringAsync(UserName,JsonSerializer.Serialize(basket),cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await _repository.StoreBasket(basket, cancellationToken);
            await _cache.SetStringAsync(basket.UserName,JsonSerializer.Serialize(basket),cancellationToken);
            return basket; 
        }
    }
}
