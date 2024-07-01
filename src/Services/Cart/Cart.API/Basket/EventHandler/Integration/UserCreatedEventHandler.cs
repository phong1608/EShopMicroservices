using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Cart.API.Basket.EventHandler.Integration
{
    public class UserCreatedEventHandler : IConsumer<UserCreatedEvent>
    {
        private readonly CartContext _context;
        public UserCreatedEventHandler(CartContext context)
        {
            _context = context;
        }
        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            var newCart = new ShoppingCart { UserId=context.Message.UserId,UserName=context.Message.UserName,CartId = Guid.NewGuid() };
            await _context.ShoppingCarts.AddAsync(newCart);
            await _context.SaveChangesAsync();
        }
    }
}
