using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.DTOs
{
    public record PaymentDTO
    {
        public string CardName { get; set; } = default!;
        public string CardNumber { get; set; } = default!;
        public string Expiration { get; set; } = default!;
        public string Cvv { get; set; } = default!;
        public int PaymentMethod { get; set; } = default!;
        public PaymentDTO(string CardName,string CardNumber,string Expiration,string Cvv,int PaymentMethod) 
        {
            this.CardName = CardName;
            this.CardNumber = CardNumber;
            this.Expiration = Expiration;
            this.Cvv = Cvv;
            this.PaymentMethod = PaymentMethod;

        }
        public PaymentDTO() { }
    }
}
