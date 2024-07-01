using Cart.API.DTOs;
using Cart.API.Exception;
using Microsoft.EntityFrameworkCore;

namespace Cart.API.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly CartContext _context;  
        public BasketRepository( CartContext context)
        {
            _context = context;
        }
        public async Task<ShoppingCart> GetBasket(Guid UserId, CancellationToken cancellationToken = default)
        {
            var basket = await _context.ShoppingCarts.Include(x=>x.Items).FirstOrDefaultAsync(x => x.UserId == UserId);

            if (basket == null)
            {
                throw new BasketNotFoundException(UserId.ToString());

            }
            return basket;


        }
        
        
        public async Task<bool> DeleteBasketItem(Guid UserId)
        {
            var userCart = await _context.ShoppingCarts
                                .Include(x=>x.Items)
                                .FirstOrDefaultAsync(x => x.UserId == UserId);

            if (userCart == null)
            {
                throw new BasketNotFoundException(UserId.ToString());
            }

            if (userCart.Items.Any())
            {
                _context.Items.RemoveRange(userCart.Items);
            }


            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateUserBasket(Guid UserId, string UserName)
        {
            var cart = new ShoppingCart() {UserId=UserId,UserName=UserName };
            await _context.ShoppingCarts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddItems(CartItemsDTO Items,string UserId)
        {
            var cart =await _context.ShoppingCarts.FirstOrDefaultAsync(x=>x.UserId==new Guid(UserId));
            if (cart == null)
            {
                throw new BasketNotFoundException(UserId);
            }
            var newItems = new ShoppingCartItem { CartId = cart.CartId, Price = Items.Price, Quantity = Items.Quantity,ProductId=Items.ProductId };
            await _context.Items.AddAsync(newItems);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
