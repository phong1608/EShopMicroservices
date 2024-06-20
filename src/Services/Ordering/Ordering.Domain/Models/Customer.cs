using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models
{
    public class Customer :Entity<Guid>
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get;private set; } = default!;
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage =("Email isn't in the right format"))]
        public string Email { get;private set; } = default!;
        public static Customer Create(Guid customerId, string name, string email)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentException.ThrowIfNullOrEmpty(email);
            return new Customer { Id = customerId, Name = name, Email = email };
        }
    }
}
