using Cart.API.Abstractions;
using Microsoft.Extensions.Caching.Distributed;

namespace Cart.API.Basket
{
    public class CacheInvalidationHandler : INotificationHandler<AddNewItemEvent>
    {
        private readonly IDistributedCache _cache;
        public CacheInvalidationHandler(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task Handle(AddNewItemEvent notification, CancellationToken cancellationToken)
        {
             await HandleInternal(notification.userId,cancellationToken);
        }

        private async Task HandleInternal(string UserId,CancellationToken cancellationToken)
        {
            await _cache.RemoveAsync(UserId, cancellationToken);
        }
    }
}
