using System;
using LibraryModel.Domain;
using LibraryModel.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace LibraryManager.Contexts
{
    public partial class LibraryDbContext : DbContext
    {
        protected IConfigurationRoot _configuration;
        public LibraryDbContext(IConfigurationRoot configuration) : base()
        {
            _configuration = configuration;
        }

        public LibraryDbContext() : base()
        { }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options) { }


        public virtual DbSet<BookEntity> bookEntity { get; set; }
     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntity>(entity =>
            {
                entity.HasKey(e => e.BookId);

                entity.Property(e => e.BookName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BookType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BookAuthor)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BookDomain>(entity =>
            {
                entity.HasKey(e => e.BookId);

                entity.Property(e => e.BookName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BookType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BookAuthor)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
