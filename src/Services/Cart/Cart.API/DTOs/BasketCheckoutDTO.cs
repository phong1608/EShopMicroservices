﻿using BuildingBlocks.Messaging.Events;

namespace Cart.API.DTOs
{
    public class BasketCheckoutDTO
    {
        public string UserName { get; set; } = default!;
        public Guid CustomerId { get; set; } = default!;
        public decimal TotalPrice { get; set; } = default!;

        // Shipping and BillingAddress
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string City { get; set; } = default!;
        public string District { get; set; } = default!;
        public string Street { get; set; } = default!;

        // Payment
        
        public CartItem Items { get; set; } = new();
    }
}
