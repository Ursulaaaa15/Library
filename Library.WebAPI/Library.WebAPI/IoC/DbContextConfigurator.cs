using Library.DataAccess;
using Library.WebAPI.Settings;
using Microsoft.EntityFrameworkCore;

namespace Library.WebAPI.IoC
{
    public class DbContextConfigurator
    {
        public static void ConfigureService(IServiceCollection services, LibrarySettings settings)
        {
            services.AddDbContextFactory<LibraryDbContext>(
                options => { options.UseSqlServer(settings.LibraryDbContextConnectionString); },
                ServiceLifetime.Scoped);
        }

        public static void ConfigureApplication(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<LibraryDbContext>>();
            using var context = contextFactory.CreateDbContext();
            context.Database.Migrate(); //makes last migrations to db and creates database if it doesn't exist
        }
    }
}