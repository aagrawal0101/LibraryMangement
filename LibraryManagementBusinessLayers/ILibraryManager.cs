using LibraryModel.Domain;
using LibraryModel.Dto;
using LibraryModel.Dto.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BusinessLayers
{
    public interface ILibraryManager
    {
        Task<BookDomain> AddNewBook(AddNewBookRequest newBookRequest);
        Task<IList<BookDomain>> GetList();
        Task<BookDomain> GetBookInfoByBookId(int bookId);
        Task<BookDomain> DeleteBookByBookId(int bookId);
        Task<BookDomain> UpdateBookByBookId(UpdateNewBookRequest updateNewBook);
    }
}
