using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models
{
    public class OrderItem :Entity<Guid>
    {
        public OrderItem(Guid orderId, Guid productId, int quantity, decimal price)
        {
            Id=new Guid();
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;

        }
        public Guid OrderId { get; private set; } = default!;
        public Guid ProductId { get; private set; } = default!;
        public int Quantity { get; private set; }= default!;
        public decimal Price { get; private set;} = default!;
    }
}
