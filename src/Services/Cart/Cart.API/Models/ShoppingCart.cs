using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cart.API.Models
{
    public class ShoppingCart
    {
        [Key]
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public virtual List<ShoppingCartItem> Items { get; set; } = default!;
        public decimal? TotalPrice =0;
        public ShoppingCart(string username)
        {
            UserName = username;
        }
        public ShoppingCart()
        {
        }

    }
}
