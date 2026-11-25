using System.ComponentModel.DataAnnotations;

namespace AspMvcLibraryWebApp.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public Author? Author { get; set; }
        public int? AuthorId { get; set; }

        public Genre? Genre { get; set; }
        public int? GenreId { get; set; }

        public Publisher? Publisher { get; set; }
        public int? PublisherId { get; set; }

        public int Year { get; set; }
        public int Pages { get; set; }

        public DateOnly AddedAt { get; set; }
    }
}
