using FluentAssertions;
using LibraryManagement.BusinessLayers;
using LibraryManagement.Mappers;
using LibraryModel.Domain;
using LibraryModel.Dto;
using LibraryModel.Dto.Contracts;
using LibraryModel.Entity;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.LibraryManagement.BusinessLayer
{
    public class AddNewBookManagerTests : ManagerTestBase
    {
        public AddNewBookManagerTests()
        {
            _libraryManager = new LibraryManagers(_libraryRepository);
        }

        [Fact]
        public async Task AddNewBook_OK()
        {
            //Arrange
            var request = new AddNewBookRequest()
            {
                NewBookDetail = new BookDto()
                {
                    pkBookId = 111,
                    BookName = "Test",
                    BookType = "1",
                    BookAuthor = "Rahul"
                }
            };
            BookDomain ValidCarts = new BookDomain
            {
                BookId = 111,
                BookName = "Test",
                BookType = "1",
                BookAuthor = "Rahul"
            };

            _libraryRepository.AddNewBook(Arg.Any<BookDomain>()).Returns(ValidCarts);

            // Act 
            var result = await _libraryManager.AddNewBook(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull("Must contain a result");
            result.BookId.Should().Be(ValidCarts.BookId);
            result.BookName.Should().Be(ValidCarts.BookName);
        }
        [Fact]
        public async Task AddNewBook_InvalidData()
        {
            //Arrange
            var request = new AddNewBookRequest()
            {
                NewBookDetail = new BookDto() { }
            };
            BookDomain ValidCarts = new BookDomain
            {
                BookId = 111,
                BookName = "Test",
                BookType = "1",
                BookAuthor = "Rahul"
            };

            _libraryRepository.AddNewBook(ValidCarts).Returns(ValidCarts);

            // Act 
            var result = await _libraryManager.AddNewBook(request).ConfigureAwait(false);

            // Assert
            result.Should().BeNull();
        }
    }
}
