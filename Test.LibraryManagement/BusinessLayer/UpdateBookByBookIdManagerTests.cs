using FluentAssertions;
using LibraryManagement.BusinessLayers;
using LibraryModel.Domain;
using LibraryModel.Dto;
using LibraryModel.Dto.Contracts;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.LibraryManagement.BusinessLayer
{
    public class UpdateBookByBookIdManagerTests : ManagerTestBase
    {
        public UpdateBookByBookIdManagerTests()
        {
            _libraryManager = new LibraryManagers(_libraryRepository);
        }

        [Fact]
        public async Task UpdateBookByBookIdManagerTests_OK()
        {
            //Arrange
            var request = new UpdateNewBookRequest()
            {
                UpdateBookDetail = new BookDto()
                {
                    pkBookId = 111,
                    BookName = "Test",
                    BookType = "1",
                    BookAuthor = "Rahul"
                }
            };
            BookDomain ValidCarts = new BookDomain
            {
                BookId = 101,
                BookName = "Test",
                BookType = "1",
                BookAuthor = "Rahul"
            };

            _libraryRepository.UpdateBookById(Arg.Any<BookDomain>()).Returns(ValidCarts);

            // Act 
            var result = await _libraryManager.UpdateBookByBookId(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull("Must contain a result");
            result.BookId.Should().Be(ValidCarts.BookId);
            result.BookName.Should().Be(ValidCarts.BookName);
        }

        [Fact]
        public async Task UpdateBookByBookIdManagerTests_InvalidData()
        {
            //Arrange
            var request = new UpdateNewBookRequest()
            {
                UpdateBookDetail = new BookDto() { }
            };
            BookDomain ValidCarts = new BookDomain { };

            _libraryRepository.UpdateBookById(Arg.Any<BookDomain>()).Returns(ValidCarts);

            // Act 
            var result = await _libraryManager.UpdateBookByBookId(request).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull("Must contain a result");


        }
    }
}
