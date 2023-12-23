using Library.BL.Books.Entities;

namespace Library.BL.Books
{
    public interface IBooksManager
    {
        BookModel CreateBook(CreateBookModel model);
        void DeleteBook(Guid positionId);
        BookModel UpdateBook (Guid positionId, UpdateBookModel model);
    }
}
