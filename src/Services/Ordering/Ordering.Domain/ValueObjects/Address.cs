using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public Address(string firstName, string lastName, string email,string phoneNumber, string city, string district, string street)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            City = city;
            District = district;
            Street = street;
        }
        public string? FirstName { get; }
        public string? LastName { get; }
        public string? Email { get; }
        public string? PhoneNumber { get; }
        public string? City { get; }
        public string? District { get; }
        public string? Street { get; }
        public static Address Of(string firstName,string lastName, string email,string phoneNumber,string city, string district,string street)
        {


            return new Address(firstName, lastName, email, phoneNumber, city, district, street);
        }
    }
}
