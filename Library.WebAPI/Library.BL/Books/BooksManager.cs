using AutoMapper;
using Library.BL.Books.Entities;
using Library.DataAccess;
using Library.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BL.Books
{
    public class BooksManager : IBooksManager
    {
        private readonly IRepository<BookEntity> _bookRepository;
        private readonly IMapper _mapper; 

        public BooksManager(IRepository<BookEntity> bookRepository, IMapper mapper) 
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        //Мне, как оказолось, провалидировать тут нечего, ну или я не догадалась
        public BookModel CreateBook(CreateBookModel model)
        {
            BookEntity entity = _mapper.Map<BookEntity>(model);

            _bookRepository.Save(entity);

            return _mapper.Map<BookModel>(entity);
        }

        public void DeleteBook(Guid bookId)
        {
            BookEntity? entity = _bookRepository.GetById(bookId);

            if (entity == null)
            {
                throw new ArgumentException("Нет позиции по заданному id");
            }

            _bookRepository.Delete(entity);
        }

        public BookModel UpdateBook(Guid positionId, UpdateBookModel model)
        { 

            BookEntity? entity = _bookRepository.GetById(positionId);

            if (entity == null)
            {
                throw new ArgumentException("Нет позиции по заданному id");
            }

            BookEntity newEntity = _mapper.Map<BookEntity>(model);
            newEntity.Id = entity.Id;
            newEntity.ExternalId = entity.ExternalId;
            newEntity.CreationTime = entity.CreationTime;
            newEntity.ModificationTime = entity.ModificationTime;

            _bookRepository.Save(newEntity);

            return _mapper.Map<BookModel>(newEntity);
        }
    }
}
