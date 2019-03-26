using FluentAssertions;
using LibraryManagement.BusinessLayers;
using LibraryModel.Entity;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.LibraryManagement.BusinessLayer
{
    public class GetBookInfoByBookIdMangerTests : ManagerTestBase
    {
        public GetBookInfoByBookIdMangerTests()
        {
            _libraryManager = new LibraryManagers(_libraryRepository);
        }

        [Fact]
        public async Task GetBookInfoByBookId_OK()
        {
            //Arrange
            int bookId = 111;

            BookEntity ValidCart = new BookEntity
            {
                BookId = 111,
                BookName = "Test",
                BookType = "1",
                BookAuthor = "Rahul"
            };

            _libraryRepository.GetBookById(bookId).Returns(ValidCart);

            // Act 
            var result = await _libraryManager.GetBookInfoByBookId(bookId).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull("Must contain a result");
            result.BookId.Should().Be(ValidCart.BookId);
            result.BookAuthor.Should().Be(ValidCart.BookAuthor);
        }

        [Fact]
        public async Task GetBookInfoByBookId_InvalidData()
        {
            //Arrange
            int bookId = 0;

            BookEntity ValidCart = new BookEntity { };

            _libraryRepository.GetBookById(bookId).Returns(ValidCart);

            // Act 
            var result = await _libraryManager.GetBookInfoByBookId(bookId).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull("Must contain a result");
            result.Should().NotBe(ValidCart);
        }

    }
}
