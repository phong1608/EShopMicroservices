using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Catalog.gRPC.Models
{
    public class ProductContext:DbContext
    {
        public ProductContext(DbContextOptions options): base(options) { }
        public ProductContext() { }
        public DbSet<Product> Products =>Set<Product>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
