using AspMvcLibraryWebApp.Areas.Dashboard.Models;
using AspMvcLibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspMvcLibraryWebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class BookController : Controller
    {
        LibraryDbContext _context = new();
        DashboardDbContext dashboardContext = new();

        public async Task<IActionResult> Index()
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Books";
            return View(await _context.Books
                                      .Include(b => b.Author)
                                      .Include(b => b.Genre)
                                      .Include(b => b.Publisher)
                                      .ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            await LoadLookupLists();
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Books";

            return View(new Book { AddedAt = DateOnly.FromDateTime(DateTime.Now) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,Year,Pages,AddedAt,Author,AuthorId,Genre,GenreId,Publisher,PublisherId")] Book book)
        {
            await LoadLookupLists();
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Books";

            if (book.AddedAt == default)
                book.AddedAt = DateOnly.FromDateTime(DateTime.Now);

            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
    
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Books";

            if (id is null) return NotFound();

            var book = await _context.Books
                                      .Include(b => b.Author)
                                      .Include(b => b.Genre)
                                      .Include(b => b.Publisher)
                                      .FirstOrDefaultAsync(b => b.Id == id);
            
            if(book is null) return NotFound();

            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Books";

            await LoadLookupLists();

            var book = await _context.Books
                                      .Include(b => b.Author)
                                      .Include(b => b.Genre)
                                      .Include(b => b.Publisher)
                                      .FirstOrDefaultAsync(b => b.Id == id);

            if (book is null) return NotFound();
            
            return View(book);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,
            [Bind("Id,Title,Year,Pages,AddedAt,Author,AuthorId,Genre,GenreId,Publisher,PublisherId")] Book book)
        {
            if (id is null) return NotFound();

            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Books";
            await LoadLookupLists();

            if (id != book.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Books";

            if (id is null) return NotFound();

            var book = await _context.Books
                                      .Include(b => b.Author)
                                      .Include(b => b.Genre)
                                      .Include(b => b.Publisher)
                                      .FirstOrDefaultAsync(b => b.Id == id);

            if (book is null) return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book is not null)
                _context.Books.Remove(book);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int? id)
        {
            return _context.Books.Any(b => b.Id == id);
        }

        private async Task LoadLookupLists()
        {
            ViewData["Authors"] = await _context.Authors.ToListAsync();
            ViewData["Genres"] = await _context.Genres.ToListAsync();
            ViewData["Publishers"] = await _context.Publishers.ToListAsync();
        }
    }
}
