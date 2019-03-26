
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LibraryModel.Domain;
using LibraryModel.Dto.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using LibraryManager.Contexts;
using LibraryManagement.Mappers.EntityDomainMapper;
using LibraryModel.Entity;
using System.Net;
using ExceptionHandlingPoc.Common;
using LibraryManagement.Common;

namespace LibraryManagement.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        public LibraryRepository(LibraryDbContext context)
        {
            Context = context;
        }

        public LibraryDbContext Context { get; }

        public async Task<BookDomain> AddNewBook(BookDomain book)
        {
            BookEntity bookEntity = EntityDomainMapper.MapEntityToDomain(book);
                var GetBookByIds = Context.bookEntity.SingleOrDefault(e => e.BookId.Equals(book.BookId));
                if (GetBookByIds != null)
                {
                    throw new BookIdAlridyPresent(HttpStatusCode.NotFound, "BookIdAlridyPresent", null);
                }

            Context.Database.EnsureCreated();
            Context.bookEntity.Add(bookEntity);
            Context.SaveChanges();
       
            return book;
        }

        public async Task<BookEntity> DeleteBookById(int bookId)
        {
                var GetBookByIds = Context.bookEntity.AsNoTracking().SingleOrDefault(e => e.BookId.Equals(bookId));
                if (GetBookByIds == null)
                {
                    throw new UserIdNotValidException(HttpStatusCode.NotFound, "StudentIdNot Found", null);
                }
                Context.bookEntity.Remove(GetBookByIds);
                Context.SaveChanges();
                return GetBookByIds;
        }

        public async Task<BookEntity> GetBookById(int bookId)
        {
                var GetBookByIds = Context.bookEntity.AsNoTracking().SingleOrDefault(e => e.BookId.Equals(bookId));
                return GetBookByIds;
        }

        public async Task<List<BookEntity>> GetBookList()
        {
                var GetBookList = Context.bookEntity.ToList();
                return GetBookList;
        }

        public async Task<BookDomain> UpdateBookById(BookDomain book)
        {
            BookEntity bookEntity = EntityDomainMapper.MapEntityToDomain(book);

                var GetBookByIds = Context.bookEntity.AsNoTracking().SingleOrDefault(e => e.BookId.Equals(book.BookId));
                if (GetBookByIds == null)
                {
                    throw new UserIdNotValidException(HttpStatusCode.NotFound, "BookIdNot Found", null);
                }
                Context.Database.EnsureCreated();
                Context.bookEntity.Update(bookEntity);
                Context.SaveChanges();
                return book;
        }

    }
}
