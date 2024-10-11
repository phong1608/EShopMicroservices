using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Cart.API.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public Guid CartId { get; set; }
        [ForeignKey("CartId")]
        [JsonIgnore]
        [IgnoreDataMember]
        public ShoppingCart Cart { get; set; } = default!;
    }
}
