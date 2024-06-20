using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public class Payment
    {
        public string? CardName { get; } = default!;
        public string? CartNumber { get; } = default!;
        public string? Expiration { get; } = default!;
        public string CVV { get; } = default!;
        public int PaymentMethod { get; } = default!;
        protected Payment() { }
        private Payment(string? cardName, string? cartNumber, string? expiration, string cVV, int paymentMethod)
        {
            CardName = cardName;
            CartNumber = cartNumber;
            Expiration = expiration;
            CVV = cVV;
            PaymentMethod = paymentMethod;
        }
        public static Payment Of(string cardName, string? cartNumber, string? expiration, string cVV, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrEmpty(cardName);
            ArgumentException.ThrowIfNullOrEmpty(cartNumber);
            ArgumentException.ThrowIfNullOrEmpty(expiration);
            
            return new Payment(cardName,cartNumber, expiration, cVV,paymentMethod);
        }
    }
}
