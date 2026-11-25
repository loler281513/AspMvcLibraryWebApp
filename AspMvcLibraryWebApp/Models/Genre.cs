using System.ComponentModel.DataAnnotations;

namespace AspMvcLibraryWebApp.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Genre Title Is Not Empty")]
        public string Title { get; set; } = null!;

        public List<Book>? Books { get; set; }
    }
}
