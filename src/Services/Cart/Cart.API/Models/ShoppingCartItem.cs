namespace Cart.API.Models
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public string  ProductName { get; set; } = string.Empty;
    }
}
