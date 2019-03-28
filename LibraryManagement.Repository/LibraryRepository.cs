
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
            var GetBookByIds = await Context.bookEntity.SingleOrDefaultAsync(e => e.BookId.Equals(book.BookId));
            if (GetBookByIds != null)
            {
                throw new BookIdAlridyPresent(HttpStatusCode.NotFound, "BookIdAlridyPresent", null);
            }

            Context.Database.EnsureCreated();
            Context.bookEntity.Add(bookEntity);
            await Context.SaveChangesAsync();

            return book;
        }

        public async Task<BookEntity> DeleteBookById(int bookId)
        {
            var GetBookByIds = await Context.bookEntity.AsNoTracking().SingleOrDefaultAsync(e => e.BookId.Equals(bookId));
            if (GetBookByIds == null)
            {
                throw new UserIdNotValidException(HttpStatusCode.NotFound, "StudentIdNot Found", null);
            }
            Context.bookEntity.Remove(GetBookByIds);
            await Context.SaveChangesAsync();
            return GetBookByIds;
        }

        public async Task<BookEntity> GetBookById(int bookId)
        {
            var GetBookByIds = await Context.bookEntity.AsNoTracking().SingleOrDefaultAsync(e => e.BookId.Equals(bookId));
            return GetBookByIds;
        }

        public async Task<List<BookEntity>> GetBookList()
        {
            var GetBookList = await Context.bookEntity.ToListAsync();
            return GetBookList;
        }

        public async Task<BookDomain> UpdateBookById(BookDomain book)
        {
            BookEntity bookEntity = EntityDomainMapper.MapEntityToDomain(book);

            var GetBookByIds = await Context.bookEntity.AsNoTracking().SingleOrDefaultAsync(e => e.BookId.Equals(book.BookId));
            if (GetBookByIds == null)
            {
                throw new UserIdNotValidException(HttpStatusCode.NotFound, "BookIdNot Found", null);
            }
            Context.Database.EnsureCreated();
            Context.bookEntity.Update(bookEntity);
            await Context.SaveChangesAsync();
            return book;
        }

    }
}
