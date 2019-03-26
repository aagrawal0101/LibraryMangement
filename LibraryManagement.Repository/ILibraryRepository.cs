using LibraryModel.Domain;
using LibraryModel.Dto.Contracts;
using LibraryModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository
{
    public interface ILibraryRepository
    {
        Task<BookDomain> AddNewBook(BookDomain book);
        Task<BookEntity> GetBookById(int bookId);
        Task<List<BookEntity>> GetBookList();
        Task<BookEntity> DeleteBookById(int bookId);
        Task<BookDomain> UpdateBookById(BookDomain book);
    }
}
