using LibraryManager.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace LZ.DataLayer.Billing.Context
{
    public class LibraryContextMemory : LibraryDbContext
    {
        public LibraryContextMemory(IConfigurationRoot configuration)
            : base(configuration)
        {
            _configuration = configuration;

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyTest").ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
