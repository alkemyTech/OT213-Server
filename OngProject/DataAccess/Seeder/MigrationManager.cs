using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OngProject.DataAccess.Seeder.AutoMigrations
{
    /*
        After executing the "Add-Migration" command to create the database snapshot, 
        you can create and populate the database just by running the application.

        Class to perform automatic migrations without commands when running the application.
        The method MigrateDatabase() in this class will run automatically the command:
            -database update.

        NuGet package required: 
            Microsoft.AspNetCore.Hosting.Abstractions

        In Program.cs implement this method in the execution line:        
            CreateHostBuilder(args).Build().MigrateDatabase().Run();
    */ 
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<OngProjectDbContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }                    
                }
            }
            return host;
        }
    }

}

