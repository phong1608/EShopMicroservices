using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Messaging.Events
{
    public class CartItem   
    {
        public Guid ProductId { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public int Quantity { get; set; } = default!;
    }
    public class BasketCheckoutEvent :IntergrationEvent
    {
        public string UserName { get; set; } = default!;
        public Guid CustomerId { get; set; } = default!;
        public decimal? TotalPrice { get; set; } = default!;

        // Shipping and BillingAddress
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string PhoneNumber { get; set; }= default!;
        public string City { get; set; } = default!;
        public string District { get; set; } = default!;
        public string Street { get; set; } = default!;

       
        //CartItem
        public List<CartItem> CartItems { get; set; } =new();
    }
}
