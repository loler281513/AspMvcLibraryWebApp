using AspMvcLibraryWebApp.Models;
using AspMvcLibraryWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspMvcLibraryWebApp.Controllers
{
    public class HomeController : Controller
    {
        LibraryDbContext context = new();

        public async Task<IActionResult> Index(SortState current, SortState property)
        {
            ViewData["Title"] = "Library Catalog";

            var books = await context.Books
                                 .Include(b => b.Author)
                                 .Include(b => b.Genre)
                                 .Include(b => b.Publisher)
                                 .OrderBy(b => b.Title)
                                 .ToListAsync();

            books = property switch
            {
                SortState.TitleDesc => books.OrderByDescending(b => b.Title).ToList(),
                SortState.AuthorAsc => books.OrderBy(b => b.Author.FullName).ToList(),
                SortState.AuthorDesc => books.OrderByDescending(b => b.Author.FullName).ToList(),
                SortState.GenreAsc => books.OrderBy(b => b.Genre.Title).ToList(),
                SortState.GenreDesc => books.OrderByDescending(b => b.Genre.Title).ToList(),
                SortState.YearAsc => books.OrderBy(b => b.Year).ToList(),
                SortState.YearDesc => books.OrderByDescending(b => b.Year).ToList(),
                _ => books.OrderBy(b => b.Title).ToList(),
            };

            IndexViewModel indexViewModel = new()
            {
                Books = books,
                SortViewModel = new SortViewModel(property)
            };

            return View(indexViewModel);
        }
    }
}
