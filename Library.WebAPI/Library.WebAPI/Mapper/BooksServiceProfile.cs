using AutoMapper;
using Library.BL.Books.Entities;
using Library.WebAPI.Controllers.Entities.Books;

namespace Library.WebAPI.Mapper
{
    public class BooksServiceProfile : Profile
    {
        public BooksServiceProfile()
        {
            CreateMap<FilterBook, BooksFilter>();
            CreateMap<CreateBookRequest, CreateBookModel>();
            CreateMap<UpdateBookRequest, UpdateBookModel>();
        }
    }
}
