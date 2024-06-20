

using Ordering.Domain.Enums;

namespace Ordering.Application.DTOs
{
    public record OrderDTO
    {

        public Guid Id { get; set; } = default!;
        public Guid CustomerId { get; set; } = default!;
        public string OrderName { get; set; } = default!;
        public AddressDTO ShippingAddress { get; set; } = default!;
        public PaymentDTO Payment { get; set; } = default!;
        public List<OrderItemDTO> Items { get; set; } = default!;
        public OrderStatus Status { get; set; } = default!;
        public OrderDTO(Guid id,Guid customerId,string orderName,AddressDTO shippingAddress,PaymentDTO payment,OrderStatus status,List<OrderItemDTO> orderItems)
        {
            Id = id;
            CustomerId=customerId;
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            Payment = payment;
            Status = status;
            Items = orderItems;

        }
        public OrderDTO() { }
        

    
    
    }
    
}
