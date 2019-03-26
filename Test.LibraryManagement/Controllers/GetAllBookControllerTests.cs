using FluentAssertions;
using LibraryManagement.BusinessLayers;
using LibraryManagement.Controllers;
using LibraryManagement.Controllers.Constants;
using LibraryModel.Domain;
using LibraryModel.Dto;
using LibraryModel.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.LibraryManagement.CommonError;
using Xunit;

namespace Test.LibraryManagement.Controllers
{
    public class GetAllBookControllerTests : ControllerTestBase
    {
        [Fact]
        public async Task GetAllBook_ok()
        {
            //Arrange
            _libraryManager.GetList().Returns(NewBook());

            //Act
            var result = (ObjectResult)await _libraryController.Get().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            result.Value.Equals(NewList);
        }

        [Fact]
        public async Task GetAllBook_InternalServerErrot()
        {
            //Arrange
            _libraryManager.When
                (x => x.GetList()).Do(x => { throw new Exception(); });

            //Act
            var result = (ObjectResult)await _libraryController.Get().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            var response = result.Value as ErrorServiceResponse;
            response.Errors.Should().NotBeEmpty();
            response.Errors.Any(x => x.Code.Equals(ApiErrorCodes.InternalServiceError))
                .Should().BeTrue();
        }

        [Fact]
        public async Task GetAllBook_NoFound()
        {
            //Arrange
            _libraryManager.GetList().ReturnsNull();
            //Act
            var result = (ObjectResult)await _libraryController.Get().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull("Must contain a result");
            var response = result.Value as ErrorServiceResponse;
            response.Errors.Should().NotBeEmpty();
            response.Errors.Any(x => x.Code.Equals(ApiErrorCodes.NotFound))
                .Should().BeTrue();
        }
    }
}
