using Library.BL.Books.Entities;

namespace Library.WebAPI.Controllers.Entities.Books
{
    public class BooksListResponse
    {
        public List<BookModel> Books { get; set; }
    }
}
