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
            var basket = await _context.ShoppingCarts.Include(x=>x.Items).FirstOrDefaultAsync(x => x.UserId == UserId, cancellationToken);

            if (basket == null)
            {
                throw new BasketNotFoundException(UserId.ToString());

            }
            return basket;


        }
        
        
        public async Task<bool> DeleteAllBasketItem(Guid UserId)
        {
            var userCart = await _context.ShoppingCarts
                                .Include(x=>x.Items)
                                .FirstOrDefaultAsync(x => x.UserId == UserId) ?? throw new BasketNotFoundException(UserId.ToString());
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

        public async Task<bool> AddItems(CartItemsDTO items, string userId)
        {
            var cart = await _context.ShoppingCarts
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.UserId == new Guid(userId));

            if (cart == null)
            {
                throw new BasketNotFoundException(userId);
            }

            var cartItem = cart.Items.FirstOrDefault(x => x.ProductId == items.ProductId);

            if (cartItem != null)
            {
                cartItem.Quantity += items.Quantity;
            }
            else
            {
                cart.Items.Add(new ShoppingCartItem
                {
                    CartId = cart.CartId,
                    Price = items.Price,
                    Quantity = items.Quantity,
                    ProductId = items.ProductId,
                    ProductName = items.ProductName
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveBasketItem(Guid UserId, Guid ProductId)
        {
            var userCart = await _context.ShoppingCarts
                                .Include(x => x.Items)
                                .FirstOrDefaultAsync(x => x.UserId == UserId);
            if (userCart == null)
            {
                throw new BasketNotFoundException(UserId.ToString());
            }
            var item =userCart.Items.FirstOrDefault(x=>x.ProductId==ProductId);   
            if (item != null)
            {
                 userCart.Items.Remove(item);
                 await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> UpdateItemQuantity(Guid UserId, Guid ProductId, int Quantity)
        {

            var userCart = await _context.ShoppingCarts
                                .Include(x => x.Items)
                                .FirstOrDefaultAsync(x => x.UserId == UserId);
            if (userCart == null)
            {
                throw new BasketNotFoundException(UserId.ToString());
            }
            var item = userCart.Items.FirstOrDefault(x => x.ProductId == ProductId);
            if(item == null)
            {
                throw new BasketItemNotFoundException(ProductId.ToString());

            }
            item.Quantity = Quantity;
            await _context.SaveChangesAsync();


            return true;
        }
    }
}
