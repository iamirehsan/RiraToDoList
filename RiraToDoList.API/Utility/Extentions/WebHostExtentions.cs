using Microsoft.EntityFrameworkCore;
using RiraToDoList.Infrastructure.Persistence.Context;


namespace RiraToDoList.API.Utility.Extentions
{
    public static class WebHostExtension
    {
        public static WebApplication SeedDatabase(this WebApplication host)
        {

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var databaseContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                databaseContext.Database.Migrate();
            }

            return host;
        }

    }



}


