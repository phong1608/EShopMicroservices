using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            

            
            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            builder.OwnsOne(
               o => o.ShippingAddress,
               sa =>
               {
                   sa.Property(a => a.FirstName).HasColumnName("Firstname");
                   sa.Property(a => a.LastName).HasColumnName("LastName");
                   sa.Property(a => a.Street).HasColumnName("ShippingStreet");
                   sa.Property(a => a.City).HasColumnName("ShippingCity");
                   sa.Property(a => a.District).HasColumnName("ShippingDistrict");
                   sa.Property(a => a.Email).HasColumnName("ShippingEmailAddress");
                   sa.Property(a => a.PhoneNumber).HasColumnName("Phone Number");

               });
            
            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    s => s.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(o => o.TotalPrice);
        }
    }
}
