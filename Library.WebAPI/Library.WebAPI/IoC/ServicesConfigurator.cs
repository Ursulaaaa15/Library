using AutoMapper;
using FoodDelivery.DataAccess;
using Library.BL.User;
using Library.DataAccess.Entities;
using Library.DataAccess;
using Microsoft.AspNetCore.Identity;
using Library.WebAPI.Settings;
using Library.BL.Books;

namespace Library.WebAPI.IoC
{
    public static class ServicesConfigurator
    {
        public static void ConfigureService(IServiceCollection services, LibrarySettings settings)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IBooksProvider>(x =>
                new BooksProvider(x.GetRequiredService<IRepository<BookEntity>>(), x.GetRequiredService<IMapper>()));

            services.AddScoped<IUsersProvider>(x =>
                new UsersProvider(x.GetRequiredService<IRepository<UserEntity>>(), x.GetRequiredService<IMapper>()));

            services.AddScoped<IBooksManager, BooksManager>();
            services.AddScoped<IUsersManager, UsersManager>();

        }
    }
}
