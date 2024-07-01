using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }     
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
