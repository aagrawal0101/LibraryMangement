using FluentAssertions;
using LibraryManagement.Repository;
using LibraryModel.Domain;
using LZ.DataLayer.Billing.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.LibraryManagement.Repository
{
    public class AddNewBookRepositoryTests : RepositoryTestBase

    {
        public AddNewBookRepositoryTests() : base()
        { }


        [Fact]
        public async Task AddNewBook_OK()
        {
            //Arrange
            BookDomain books = new BookDomain();

            var entity = Create<BookDomain>(new BookDomain
            {
                BookId = 101,
                BookAuthor = "Rahul",
                BookName = "c#",
                BookType = "1",
            });

            //Act
            using (var _context = new LibraryContextMemory(_configuration))
            {
                var libraryRepository = new LibraryRepository(_context);
                var result = await libraryRepository.AddNewBook(entity);
                // Assert
                result.Should().NotBeNull("Must contain a result");
            }
        }
    }
}
