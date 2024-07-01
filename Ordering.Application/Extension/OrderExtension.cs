using Catalog.gRPC.Protos;
using Ordering.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.OrdersExtension
{
    public static class OrderExtension
    {
        public static IEnumerable<OrderDTO> ToOrderDtoList(this IEnumerable<Order> orders)
        {
            
            return orders.Select(order => new OrderDTO(
                id: order.Id,
                customerId: order.CustomerId,
                orderName: order.OrderName!,
                shippingAddress: new AddressDTO(order.ShippingAddress.FirstName!, order.ShippingAddress.LastName!, order.ShippingAddress.Email!, order.ShippingAddress.PhoneNumber!, order.ShippingAddress.City!, order.ShippingAddress.District!, order.ShippingAddress.Street!),
                status: order.Status,
                orderItems: order.OrderItems.Select(oi => new OrderItemDTO(oi.OrderId, oi.ProductId, oi.Quantity, oi.Price)).ToList()
            ));
        }
        public static OrderDTO ToOrderDTO(this Order order)
        {
            return new OrderDTO(
                id: order.Id,
                customerId: order.CustomerId,
                orderName: order.OrderName!,
                shippingAddress: new AddressDTO(order.ShippingAddress.FirstName!, order.ShippingAddress.LastName!, order.ShippingAddress.Email!, order.ShippingAddress.PhoneNumber!, order.ShippingAddress.City!, order.ShippingAddress.District!, order.ShippingAddress.Street!),
                status: order.Status,
                orderItems: order.OrderItems.Select(oi => new OrderItemDTO(oi.OrderId, oi.ProductId, oi.Quantity, oi.Price)).ToList()
            );
        }
    }
    public class OrderItemExtension
    {
        private readonly GetProductService.GetProductServiceClient _client;
        public OrderItemExtension(GetProductService.GetProductServiceClient client)
        {
            _client = client;
        }

        public async Task<string>  GetProductName(string ProductId)
        {
            var product = await _client.GetProuductInfoAsync(new GetProductRequest { Id = ProductId });

            return product.ProductName;
        }
    }

}
