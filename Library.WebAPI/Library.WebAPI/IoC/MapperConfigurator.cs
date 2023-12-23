using Library.BL.Mapper;
using Library.WebAPI.Mapper;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Library.WebAPI.IoC
{
    public class MapperConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<BooksBLProfile>();
                config.AddProfile<UsersBLProfile>();
                config.AddProfile<BooksServiceProfile>();
                config.AddProfile<UsersServiceProfile>();
            });
        }
    }
}
