using LibraryManagement.BusinessLayers;
using LibraryManagement.Controllers;
using LibraryModel.Domain;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Test.LibraryManagement.CommonError;

namespace Test.LibraryManagement.Controllers
{
    public class ControllerTestBase
    {
        public ILibraryManager _libraryManager;
        public LibraryController _libraryController;
        public HttpContext _httpContextDefault = new DefaultHttpContext();

        public IList<BookDomain> NewList;

        public IList<BookDomain> NewBook()
        {
            NewList = new List<BookDomain>();
            NewList.Add(new BookDomain { BookId = 111, BookAuthor = "Rahul", BookName = "c#", BookType = "1" });
            return NewList;
        }

        public ControllerTestBase()
        {
            _libraryManager = Substitute.For<ILibraryManager>();
            _libraryController = new LibraryController(_libraryManager);
            _libraryController.ControllerContext.HttpContext = _httpContextDefault;
        }
    }
}
