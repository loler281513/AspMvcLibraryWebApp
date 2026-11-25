using System.ComponentModel.DataAnnotations;

namespace AspMvcLibraryWebApp.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string? Logo { get; set; }

        public List<Book> Books { get; set; } = new();
    }
}
