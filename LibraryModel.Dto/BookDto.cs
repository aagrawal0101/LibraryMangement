using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryModel.Dto
{
    public class BookDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int pkBookId { get; set; }
        public string BookName { get; set; }
        public string BookType { get; set; }
        public string BookAuthor { get; set; }
    }
}
