using AutoMapper;
using Library.BL.User.Entites;
using Library.WebAPI.Controllers.Entities.Users;

namespace Library.WebAPI.Mapper
{
    public class UsersServiceProfile : Profile
    {
        public UsersServiceProfile()
        {
            CreateMap<UpdateUserRequest, UpdateUserModel>();
            CreateMap<RegisterUserRequest, RegisterUserModel>();
        }
    }
}
