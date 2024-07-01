namespace Catalog.API.Models.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
