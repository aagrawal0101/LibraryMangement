using FluentAssertions;
using LibraryManagement.Repository;
using LibraryModel.Domain;
using LibraryModel.Entity;
using LZ.DataLayer.Billing.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.LibraryManagement.Repository
{
    public class UpdateBookByIdRepositoryTests : RepositoryTestBase
    {
        public UpdateBookByIdRepositoryTests() : base()
        { }

        [Fact]
        public async Task UpdateBookById_OK()
        {
            //Arrange

            var entity = Create<BookEntity>(x =>
                      {
                          x.BookId = 101;
                          x.BookAuthor = "Priya";
                          x.BookName = "DC";
                          x.BookType = "2";
                      });

            var book = new BookDomain()
            {
                BookId = 101,
            };

            //Act
            using (var _context = new LibraryContextMemory(_configuration))
            {
                var libraryRepository = new LibraryRepository(_context);
                var result = await libraryRepository.UpdateBookById(book);
                // Assert
                result.Should().NotBeNull("Must contain a result");
                result.BookId.Equals(101);
            }
        }
    }
}
