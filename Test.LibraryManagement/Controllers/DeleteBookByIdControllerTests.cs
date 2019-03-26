using FluentAssertions;
using LibraryManagement.Controllers.Constants;
using LibraryModel.Domain;
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
    public class DeleteBookByIdControllerTests : ControllerTestBase
    {
        public BookDomain ValidCarts => new BookDomain();

        [Fact]
        public async Task DeleteBookById_ok()
        {
            //Arrange
            int bookId = 111;
            _libraryManager.DeleteBookByBookId(Arg.Any<int>()).Returns(ValidCarts);

            //Act
            var result = (ObjectResult)await _libraryController.DeleteBookById(bookId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            result.Value.Equals(NewList);
        }

        [Fact]
        public async Task DeleteBookById_BookIdRequestIsInvalid()
        {
            //Arrange
            int bookId = 1;
           
            //Act
            var result = (ObjectResult)await _libraryController.DeleteBookById(bookId).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            var response = result.Value as ErrorServiceResponse;
            response.Errors.Should().NotBeEmpty();
            response.Errors.Any(x => x.Code.Equals(ApiErrorCodes.BookIdRequestIsInvalid))
               .Should().BeTrue();
        }


        [Fact]
        public async Task DeleteBookById_InternalServerErrot()
        {
            //Arrange
            _libraryManager.When
                (x => x.DeleteBookByBookId(Arg.Any<int>())).Do(x => { throw new Exception(); });

            //Act
            var result = (ObjectResult)await _libraryController.DeleteBookById(Arg.Any<int>()).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            var response = result.Value as ErrorServiceResponse;
            response.Errors.Should().NotBeEmpty();
            response.Errors.Any(x => x.Code.Equals(ApiErrorCodes.InternalServiceError))
                .Should().BeFalse();
        }
    }
}
