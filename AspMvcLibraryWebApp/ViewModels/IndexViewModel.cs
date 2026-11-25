using AspMvcLibraryWebApp.Models;

namespace AspMvcLibraryWebApp.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
        public SortViewModel SortViewModel { get; set; } = new(SortState.TitleAsc);
    }
}
