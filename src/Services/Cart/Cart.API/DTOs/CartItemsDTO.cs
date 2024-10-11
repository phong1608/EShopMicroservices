namespace Cart.API.DTOs
{
    public class CartItemsDTO
    {
        public Guid ProductId { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string ProductName { get; set; } = default!;
    }
    public static class ShoppingCartItemExtension
    {
        public static CartItemsDTO ToCartItemsDTO(this ShoppingCartItem CartItem)
        {
            return new CartItemsDTO { ProductId = CartItem.ProductId, Price = CartItem.Price, Quantity = CartItem.Quantity,ProductName = CartItem.ProductName };
        }
    }
}
