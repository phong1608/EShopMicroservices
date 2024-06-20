using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models
{
    public class Product :Entity<Guid>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
        public static Product Create(Guid productId, string name, decimal price)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            var product = new Product
            {
                Id = productId,
                Name = name,
                Price = price
            };
            return product;
        }
    }
}
