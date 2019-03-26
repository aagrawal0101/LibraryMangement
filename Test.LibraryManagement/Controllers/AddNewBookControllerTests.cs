using FluentAssertions;
using LibraryManagement.Common;
using LibraryManagement.Controllers.Constants;
using LibraryModel.Domain;
using LibraryModel.Dto;
using LibraryModel.Dto.Contracts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Test.LibraryManagement.CommonError;
using Xunit;

namespace Test.LibraryManagement.Controllers
{
    public class AddNewBookControllerTests : ControllerTestBase
    {
        public BookDomain validBook => new BookDomain();

        [Fact]
        public async Task PostAddNewBook_InternalServerError()
        {
            //Arrange
            var request = new AddNewBookRequest()
            {
                NewBookDetail = new BookDto()
                {
                    pkBookId = 123,
                    BookName = "C++",
                    BookAuthor = "Radha",
                    BookType = "1"
                }
            };

            _libraryManager.When
                         (x => x.AddNewBook(Arg.Any<AddNewBookRequest>())).Do(x => { throw new Exception(); });
            //Act
            var result = (ObjectResult)await _libraryController.AddNewBook(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            var response = result.Value as ErrorServiceResponse;
            response.Errors.Should().NotBeEmpty();
            response.Errors.Any(x => x.Code.Equals(ApiErrorCodes.InternalServiceError))
               .Should().BeTrue();
        }
        [Fact]
        public async Task PostAddNewBook_OK()
        {
            //Arrange
            var request = new AddNewBookRequest()
            {
                NewBookDetail = new BookDto()
                {
                    pkBookId = 123,
                    BookName = "C++",
                    BookAuthor = "Radha",
                    BookType = "1"
                }
            };
            var AddBooks = new BookDomain()
            {
                BookId = 123,
                BookName = "C++",
                BookAuthor = "Radha",
                BookType = "1"

            };
            _libraryManager.AddNewBook(Arg.Any<AddNewBookRequest>()).Returns(AddBooks);

            //Act

            var result = (ObjectResult)await _libraryController.AddNewBook(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            result.Value.Equals(AddBooks);
        }

        [Fact]
        public async Task PostAddNewBook_BadRequest()
        {
            //Arrange
            var request = new AddNewBookRequest()
            {
                NewBookDetail = new BookDto()
                {
                    pkBookId = 123,
                    BookName = "C++",
                    BookAuthor = "Radha",
                    BookType = "1"
                }
            };
            //Act
            var result = (ObjectResult)await _libraryController.AddNewBook(request).ConfigureAwait(false);


            //Assert
            result.Should().NotBeNull("Must contain a result");
            var response = result.Value as ErrorServiceResponse;
            response.Errors.Should().NotBeEmpty();
            response.Errors.Any(x => x.Code.Equals(ApiErrorCodes.BadRequest))
               .Should().BeTrue();
        }

        [Fact]
        public async Task PostAddNewBook_BookIdAlridyPresent()
        {
            //Arrange
            var request = new AddNewBookRequest()
            {
                NewBookDetail = new BookDto()
                {
                    pkBookId = 123,
                    BookName = "C++",
                    BookAuthor = "Radha",
                    BookType = "1"
                }
            };

            _libraryManager.When
               (x => x.AddNewBook(Arg.Any<AddNewBookRequest>())).Do(x => { throw new BookIdAlridyPresent(HttpStatusCode.NotFound, "book_not_found", null); });
            //Act
            var result = (ObjectResult)await _libraryController.AddNewBook(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
        }
    }
}
