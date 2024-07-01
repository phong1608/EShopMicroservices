
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extension
{
    public static class DatabaseExtension
    {
        public static void InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope=app.Services.CreateScope();
            var context=scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();
        }
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            
            await SeedOrdersAsync(context);
        }
        
        private static async Task SeedOrdersAsync(ApplicationDbContext context)
        {
            if (!await context.Orders.AnyAsync())
            {
                await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
                await context.SaveChangesAsync();
            }
        }
    }
}
