using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Cart.API.Models
{
    public class CartContext:DbContext
    {
        public CartContext(DbContextOptions options):base(options) { }
        public CartContext() { }
        public DbSet<ShoppingCart> ShoppingCarts =>Set<ShoppingCart>();
        public DbSet<ShoppingCartItem> Items =>Set<ShoppingCartItem>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }

    }
}
