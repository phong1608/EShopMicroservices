﻿

namespace Ordering.Application.DTOs
{
    public record OrderItemDTO
    {
        public Guid OrderId { get; set; } = default!;
        public Guid ProductId { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public OrderItemDTO(Guid OrderId, Guid ProductId, int Quantity, decimal Price,string ProductName="")
        {
            this.OrderId = OrderId;
            this.ProductId = ProductId;
            this.Quantity = Quantity;
            this.Price = Price;
            this.ProductName = ProductName;
        }
        public OrderItemDTO() { }
    }
}
