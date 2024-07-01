using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.Events;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models
{
    public class Order :Aggregate<Guid>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public Guid CustomerId { get; private set; } = default!;
        public string OrderName { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        public static Order Create(Guid orderId,Guid customerId,string orderName,Address shippingAddress,OrderStatus orderStatus=OrderStatus.Pending)
        {
            var order = new Order()
            {
                Id= orderId,
                CustomerId=customerId,
                OrderName=orderName,
                ShippingAddress=shippingAddress,
                Status=orderStatus
            };
            order.AddDomainEvent(new OrderCreatedEvent(order));
            return order;
        }
        public void Update(Guid orderId, Guid customerId, string orderName, Address shippingAddress,  OrderStatus orderStatus)
        {
            Id = orderId;
            CustomerId = customerId;
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            Status = orderStatus;
            AddDomainEvent(new UpdateOrderEvent());
        }
        public void Add(Guid productId,int quantity,decimal price)
        {
            var orderItem = new OrderItem(Id,productId,quantity,price);
            _orderItems.Add(orderItem);
        }
        public void Remove(Guid productId)
        {
            var orderItem = _orderItems.FirstOrDefault(x=>x.ProductId==productId);  
            if (orderItem != null)
            {
                _orderItems.Remove(orderItem);

            }
        }

    }
}
