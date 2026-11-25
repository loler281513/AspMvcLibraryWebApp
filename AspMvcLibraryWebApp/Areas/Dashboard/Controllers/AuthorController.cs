using AspMvcLibraryWebApp.Areas.Dashboard.Models;
using AspMvcLibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspMvcLibraryWebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AuthorController : Controller
    {
        LibraryDbContext _context = new();
        DashboardDbContext dashboardContext = new();

        // GET: AUTHORS
        public async Task<IActionResult> Index()
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Authors";

            return View(await _context.Authors
                                      .Include(a => a.Genre)
                                      .ToListAsync());
        }

        // GET: AUTHORS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Authors";

            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                                       .Include(a => a.Genre)
                                       .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: AUTHORS/Create
        public async Task<IActionResult> Create()
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Authors";
            ViewData["Genres"] = await _context.Genres.ToListAsync();

            return View();
        }

        // POST: AUTHORS/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Country,Genre,GenreId")] Author author)
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Authors";
            ViewData["Genres"] = await _context.Genres.ToListAsync();

            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: AUTHORS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Authors";

            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            ViewData["Genres"] = await _context.Genres.ToListAsync();

            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: AUTHORS/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,FullName,Country,Genre,GenreId")] Author author)
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Authors";
            ViewData["Genres"] = await _context.Genres.ToListAsync();

            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: AUTHORS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Authors";

            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                                       .Include(a => a.Genre)
                                       .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: AUTHORS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int? id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
