using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Mappers;
using LibraryManagement.Mappers.EntityDomainMapper;
using LibraryManagement.Repository;
using LibraryModel.Domain;
using LibraryModel.Dto.Contracts;
using LibraryModel.Entity;

namespace LibraryManagement.BusinessLayers
{
    public class LibraryManagers : ILibraryManager
    {
        ILibraryRepository _libraryRepository;
        public LibraryManagers(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }
        public async Task<BookDomain> AddNewBook(AddNewBookRequest newBookRequest)
        {
            BookDomain book = DtoDomainMapper.MapDtoToDomain(newBookRequest);
            var AddBooks = await _libraryRepository.AddNewBook(book).ConfigureAwait(false);
            return AddBooks;
        }

        public async Task<IList<BookDomain>> GetList()
        {
            IList<BookDomain> BookInformation = null;
            var bookEntitie = await _libraryRepository.GetBookList().ConfigureAwait(false);
            if (bookEntitie != null)
            {
                BookInformation = EntityDomainMapper.MapEntityListToDomain(bookEntitie);
            }
            return BookInformation;
        }

        public async Task<BookDomain> GetBookInfoByBookId(int bookId)
        {
            BookDomain BookInfo = null;
            BookEntity bookEntity = await _libraryRepository.GetBookById(bookId).ConfigureAwait(false);

            if (bookEntity != null)
            {
                BookInfo = EntityDomainMapper.MapEntityToDomains(bookEntity);
            }
            return BookInfo;
        }

        public async Task<BookDomain> DeleteBookByBookId(int bookId)
        {
            BookDomain BookInfo = null;
            BookEntity bookEntity = await _libraryRepository.DeleteBookById(bookId).ConfigureAwait(false);

            if (bookEntity != null)
            {
                BookInfo = EntityDomainMapper.MapEntityToDomains(bookEntity);
            }
            return BookInfo;
        }

        public async Task<BookDomain> UpdateBookByBookId(UpdateNewBookRequest updateNewBook)
        {
            BookDomain book = DtoDomainMapper.MapDtoToDomainToUpdateBook(updateNewBook);
            var UpdateBook = await _libraryRepository.UpdateBookById(book).ConfigureAwait(false);
            return UpdateBook;
        }
    }

}


