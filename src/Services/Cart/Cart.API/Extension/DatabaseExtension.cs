using Microsoft.EntityFrameworkCore;

namespace Cart.API.Extension
{
    public static class DatabaseExtension
    {
        public static void InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CartContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();
        }
    }
}
