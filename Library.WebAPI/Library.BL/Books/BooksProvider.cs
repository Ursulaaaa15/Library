using AutoMapper;
using Library.BL.Books.Entities;
using Library.DataAccess;
using Library.DataAccess.Entities;

namespace Library.BL.Books
{
    public class BooksProvider : IBooksProvider
    {
        private readonly IRepository<BookEntity> _bookRepository;
        private readonly IMapper _mapper;
        public BooksProvider(IRepository<BookEntity> booksRepository, IMapper mapper) 
        {
            _bookRepository = booksRepository;
            _mapper = mapper;
        }
        public BookModel GetBookInfo(Guid bookId)
        {
            BookEntity? book = _bookRepository.GetById(bookId);

            if (book == null)
            {
                throw new ArgumentException("Нет позиции по заданному id");
            }

            return _mapper.Map<BookModel>(book);
        }

        public IEnumerable<BookModel> GetBooks(BooksFilter filter)
        {
            var autor = filter?.Autor;
            var genre = filter?.Genre;

            var books = _bookRepository.GetAll(x =>
                (autor == null || x.Autor == autor) &&
                (genre == null || x.Genre == genre));

            return _mapper.Map<IEnumerable<BookModel>>(books);
        }
    }
}
