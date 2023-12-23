using AutoMapper;
using Library.BL.User.Entites;
using Library.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BL.Mapper
{
    public class UsersBLProfile : Profile
    {
        public UsersBLProfile()
        {
            CreateMap<UserEntity, UserModel>()
                .ForMember(x => x.Id, y => y.MapFrom(src => src.ExternalId))
                .ForMember(x => x.FullName, y => y.MapFrom(src => $"{src.FirstName} {src.SecondName} {src.Patronymic}"));

            CreateMap<UpdateUserModel, UserEntity>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.ExternalId, y => y.Ignore())
                .ForMember(x => x.CreationTime, y => y.Ignore())
                .ForMember(x => x.ModificationTime, y => y.Ignore());
        }
    }
}
