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
    public class GetBookByIdRepositoryTests : RepositoryTestBase
    {
        public GetBookByIdRepositoryTests() : base()
        { }
        [Fact]
        public async Task GetBookById_OK()
        {
            //Arrange
            var entity = Create<BookEntity>(x =>
                       {
                           x.BookId = Math.Abs(Guid.NewGuid().GetHashCode());
                       });
            //Act
            using (var _context = new LibraryContextMemory(_configuration))
            {
                var libraryRepository = new LibraryRepository(_context);
                var result = await libraryRepository.GetBookById(entity.BookId);
                // Assert
                result.Should().NotBeNull("Must contain a result");
                result.BookId.Equals(101);
            }
        }
    }
}
