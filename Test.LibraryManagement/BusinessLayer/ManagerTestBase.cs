using LibraryManagement.BusinessLayers;
using LibraryManagement.Mappers;
using LibraryManagement.Repository;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.LibraryManagement.BusinessLayer
{
    public class ManagerTestBase
    {
        protected ILibraryManager _libraryManager;
        protected ILibraryRepository _libraryRepository;

        public ManagerTestBase() : base()
        {
            _libraryManager = Substitute.For<ILibraryManager>();
            _libraryRepository = Substitute.For<ILibraryRepository>();
            DtoDomainMapper.ConfigMapper();
        }
    }
}
