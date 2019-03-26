using FluentAssertions;
using LibraryManagement.Controllers.Constants;
using LibraryModel.Domain;
using LibraryModel.Dto;
using LibraryModel.Dto.Contracts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.LibraryManagement.CommonError;
using Xunit;

namespace Test.LibraryManagement.Controllers
{
    public class UpdateBookByIdControllerTests : ControllerTestBase
    {
        [Fact]
        public async Task UpdateBookById_OK()
        {
            //Arrange
            var request = new UpdateNewBookRequest()
            {
                UpdateBookDetail = new BookDto()
                {
                    pkBookId = 122,
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

            _libraryManager.UpdateBookByBookId(Arg.Any<UpdateNewBookRequest>()).Returns(AddBooks);

            //Act

            var result = (ObjectResult)await _libraryController.UpdateBookById(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            result.Value.Equals(AddBooks);
        }
        [Fact]
        public async Task UpdateBookById_NotFound()
        {
            //Arrange
            var request = new UpdateNewBookRequest()
            {
                UpdateBookDetail = new BookDto()
                {
                    pkBookId = 122,
                    BookName = "C++",
                    BookAuthor = "Radha",
                    BookType = "1"
                }
            };

            //Act
            var result = (ObjectResult)await _libraryController.UpdateBookById(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            var response = result.Value as ErrorServiceResponse;
            response.Errors.Any(x => x.Code.Equals(ApiErrorCodes.BookIdRequestIsInvalid))
              .Should().BeTrue();
        }

        [Fact]
        public async Task UpdateBookById_InternalServerError()
        {
            //Arrange
            var request = new UpdateNewBookRequest()
            {
                UpdateBookDetail = new BookDto()
                {
                    pkBookId = 122,
                    BookName = "C++",
                    BookAuthor = "Radha",
                    BookType = "1"
                }
            };

            _libraryManager.When
                 (x => x.UpdateBookByBookId(Arg.Any<UpdateNewBookRequest>())).Do(x => { throw new Exception(); });
            //Act
            var result = (ObjectResult)await _libraryController.UpdateBookById(request).ConfigureAwait(false);


            //Assert
            result.Should().NotBeNull("Must contain a result");
            var response = result.Value as ErrorServiceResponse;
            response.Errors.Should().NotBeEmpty();
            response.Errors.Any(x => x.Code.Equals(ApiErrorCodes.InternalServiceError))
               .Should().BeTrue();
        }
    }
}

