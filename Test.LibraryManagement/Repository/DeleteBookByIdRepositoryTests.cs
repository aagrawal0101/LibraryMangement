using FluentAssertions;
using LibraryManagement.Controllers.Constants;
using LibraryManagement.Repository;
using LibraryModel.Entity;
using LZ.DataLayer.Billing.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.LibraryManagement.Repository
{
    public class DeleteBookByIdRepositoryTests : RepositoryTestBase
    {
        public DeleteBookByIdRepositoryTests() : base()
        { }

        [Fact]
        public async Task DeleteBookById_OK()
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
                var result = await libraryRepository.DeleteBookById(entity.BookId);
                // Assert
                result.Should().NotBeNull("Must contain a result");
                result.BookId.Equals(110);
            }
        }
    }
}
