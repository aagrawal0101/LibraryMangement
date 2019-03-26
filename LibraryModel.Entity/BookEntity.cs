using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryModel.Entity
{
    public class BookEntity
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookType { get; set; }
        public string BookAuthor { get; set; }
    }
}
