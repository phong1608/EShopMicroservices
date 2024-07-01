namespace Cart.API.DTOs
{
    public class CartResponseDTO
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public virtual List<CartItemsDTO> CartItemDTO { get; set; } = default!;
        public decimal? TotalPrice;

    }
   
}
