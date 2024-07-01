using Authenticate_Service.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authenticate.API.Models
{
    public class UserContext:IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public UserContext(DbContextOptions options) : base(options) { }
        public UserContext() { }
        public DbSet<ApplicationUser> User =>Set<ApplicationUser>();
        public DbSet<ApplicationRole> Role =>Set<ApplicationRole>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Thêm dữ liệu cho ApplicationRole
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = new Guid("1a777a58-fa91-469b-8408-c7f403292820"),
                    Name = "Customer",
                    NormalizedName = "customer"
                },
                new ApplicationRole
                {
                    Id = new Guid("899441f8-9863-43fe-ae5e-e7cd8a24f26e"),
                    Name = "Admin",
                    NormalizedName = "admin"
                }
            );

            

            base.OnModelCreating(modelBuilder);
        }

    }
}
