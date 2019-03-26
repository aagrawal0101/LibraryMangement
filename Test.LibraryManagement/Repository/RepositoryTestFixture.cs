using LibraryManagement.BusinessLayers;
using LibraryManagement.Controllers;
using LibraryManagement.Mappers;
using LibraryManagement.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.LibraryManagement.Repository
{
    public class RepositoryTestFixture
    {
        protected static IConfigurationRoot _configuration;
        protected IMemoryCache _memoryCache;
        private readonly IDistributedCache _cache;
        public LibraryController _libraryController;
        protected ILibraryManager _libraryManager;
        public HttpContext _httpContextDefault = new DefaultHttpContext();

        public RepositoryTestFixture() : base()
        {
            _libraryManager = Substitute.For<ILibraryManager>();
            _libraryController = new LibraryController(_libraryManager);
            _libraryController.ControllerContext.HttpContext = _httpContextDefault;
            _configuration = new ConfigurationBuilder()
                 .Build();
            DtoDomainMapper.ConfigMapper();
        }
    }
}
