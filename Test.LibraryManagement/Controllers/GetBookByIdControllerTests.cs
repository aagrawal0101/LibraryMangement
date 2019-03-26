using FluentAssertions;
using LibraryManagement.Controllers.Constants;
using LibraryModel.Domain;
using LibraryModel.Entity;
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
    public class GetBookByIdControllerTests : ControllerTestBase
    {
        public BookDomain ValidCarts => new BookDomain();
        [Fact]
        public async Task GetBookById_ok()
        {
            //Arrange
            int bookId = 111;
            _libraryManager.DeleteBookByBookId(Arg.Any<int>()).Returns(ValidCarts);

            //Act
            var result = (ObjectResult)await _libraryController.GetBookById(bookId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            result.Value.Equals(NewList);
        }

        [Fact]
        public async Task GetBookById_NotFound()
        {
            //Arrange
            int bookId = 11;
            _libraryManager.DeleteBookByBookId(bookId).Returns(ValidCarts);
            //Act
            var result = (ObjectResult)await _libraryController.GetBookById(bookId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            var response = result.Value as ErrorServiceResponse;
            response.Errors.Should().NotBeEmpty();
            response.Errors.Any(x => x.Code.Equals(ApiErrorCodes.BookIdRequestIsInvalid))
               .Should().BeTrue();
        }

        [Fact]
        public async Task GetBookById_InternalServerErrot()
        {
            //Arrange
            _libraryManager.When
                (x => x.GetBookInfoByBookId(Arg.Any<int>())).Do(x => { throw new Exception(); });

            //Act
            var result = (ObjectResult)await _libraryController.GetBookById(Arg.Any<int>()).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            var response = result.Value as ErrorServiceResponse;
            response.Errors.Should().NotBeEmpty();
            response.Errors.Any(x => x.Code.Equals(ApiErrorCodes.InternalServiceError))
                .Should().BeFalse();
        }

        [Fact]
        public async Task GetBookById_BookIdLessThanZero()
        {
            //Arrange
            int bookId = 0;

            //Act
            var result = (ObjectResult)await _libraryController.GetBookById(bookId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            var response = result.Value as ErrorServiceResponse;
            response.Errors.Should().NotBeEmpty();
            response.Errors.Any(x => x.Code.Equals(ApiErrorCodes.NotFound))
               .Should().BeTrue();
        }

        [Fact]
        public async Task GetBookById_ModeStateValidationFailed()
        {
            //Arrange
            var bookId = -1;
           _libraryController.ModelState.AddModelError("bookId", "invalidbookId");

            //Act
            var result = await _libraryController.GetBookById(bookId).ConfigureAwait(false) as ObjectResult;

            //Assert
            result.Should().NotBeNull("Must contain a result");
            var response = result.Value as ErrorServiceResponse;
            response.Errors.Should().NotBeEmpty();
            response.Errors.Any(x => x.Code.Equals(ApiErrorCodes.BadRequest))
               .Should().BeFalse();
        }
    }
}
