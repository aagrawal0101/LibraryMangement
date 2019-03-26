using FluentAssertions;
using LibraryManagement.BusinessLayers;
using LibraryModel.Entity;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.LibraryManagement.Controllers;
using Xunit;

namespace Test.LibraryManagement.BusinessLayer
{
    public class GetListManagerTests : ManagerTestBase
    {
        public GetListManagerTests() : base()
        {
            _libraryManager = new LibraryManagers(_libraryRepository);
        }

        [Fact]
        public async Task GetListManagerTests_Ok()
        {
            //Arrange
            List<BookEntity> ValidCart = new List<BookEntity>();
            ValidCart.Add(new BookEntity()
            {
                BookId = 111,
                BookName = "Test",
                BookType = "1",
                BookAuthor = "Rahul"
            });
            _libraryRepository.GetBookList().Returns(ValidCart);

            // Act 
            var result = await _libraryManager.GetList().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull("Must contain a result");
            result.ToList().Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetListManagerTests_InvalidData()
        {
            //Arrange
            List<BookEntity> ValidCart = new List<BookEntity>();
            _libraryRepository.GetBookList().Returns(ValidCart);

            // Act 
            var result = await _libraryManager.GetList().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull("Must contain a result");
            result.Should().BeEmpty();
        }
    }
}

