using FluentAssertions;
using LibraryManagement.Repository;
using LibraryModel.Entity;
using LZ.DataLayer.Billing.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.LibraryManagement.Repository
{
    public class GetAllBookRepositoryTests : RepositoryTestBase
    {
        public GetAllBookRepositoryTests() : base()
        { }

        [Fact]
        public async Task GetAllBook_OK()
        {
            //Arrange

            //Act
            using (var _context = new LibraryContextMemory(_configuration))
            {
                var libraryRepository = new LibraryRepository(_context);
                var result = await libraryRepository.GetBookList();
                // Assert
                result.Should().NotBeNull("Must contain a result");
            }
        }
    }
}

