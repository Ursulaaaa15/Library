using Library.BL.Books.Entities;

namespace Library.BL.Books
{
    public interface IBooksProvider
    {
        IEnumerable<BookModel> GetBooks(BooksFilter? filter = null);
        BookModel GetBookInfo(Guid bookId);
    }
}
