using System.ComponentModel.DataAnnotations;

namespace AspMvcLibraryWebApp.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        public string? Country { get; set; }

        public Genre? Genre { get; set; }
        public int? GenreId { get; set; }
    }
}
