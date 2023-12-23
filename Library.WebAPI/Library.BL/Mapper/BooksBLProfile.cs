using AutoMapper;
using Library.BL.Books.Entities;
using Library.DataAccess.Entities;
namespace Library.BL.Mapper
{
    public class BooksBLProfile : Profile
    {
        public BooksBLProfile() 
        {
            CreateMap<BookEntity, BookModel>()
                .ForMember(x => x.Id, y => y.MapFrom(src => src.ExternalId));

            CreateMap<CreateBookModel, BookEntity>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.ExternalId, y => y.Ignore())
                .ForMember(x => x.ModificationTime, y => y.Ignore())
                .ForMember(x => x.CreationTime, y => y.Ignore());

            CreateMap<UpdateBookModel, BookEntity>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.ExternalId, y => y.Ignore())
                .ForMember(x => x.ModificationTime, y => y.Ignore())
                .ForMember(x => x.CreationTime, y => y.Ignore());
        }
    }
}
