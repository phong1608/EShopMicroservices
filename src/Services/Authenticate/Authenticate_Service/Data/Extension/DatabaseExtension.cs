using Authenticate.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Authenticate.API.Data.Extension
{
    public static class DatabaseExtension
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<UserContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();
            await SeedAsync(context);
        }
        public static async Task  SeedAsync(UserContext context)
        {
            await SeedRoleData(context);

        }
       
        public static async Task SeedRoleData(UserContext context)
        {
            if (!await context.Role.AnyAsync())
            {
                await context.Role.AddRangeAsync(InitialData.Roles);
                await context.SaveChangesAsync();
            }
        }
    }
}
