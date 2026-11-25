using AspMvcLibraryWebApp.Areas.Dashboard.Models;
using AspMvcLibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspMvcLibraryWebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class GenreController : Controller
    {
        LibraryDbContext libraryContext = new();
        DashboardDbContext dashboardContext = new();


        // GET All
        public async Task<IActionResult> Index()
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Genres";

            return View(await libraryContext.Genres.ToListAsync());
        }

        // GET Create
        public async Task<IActionResult> Create()
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Genres";
            return View();
        }

        // POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Title")] Genre genre)
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Genres";

            if (ModelState.IsValid)
            {
                var isUnique = libraryContext.Genres.All(g => g.Title != genre.Title);

                if(!isUnique)
                {
                    ModelState.AddModelError("Title", "Название жанра должно быть уникальным");
                    return View(genre);
                }

                libraryContext.Add(genre);
                await libraryContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(genre);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Genres";

            if (id is null)
                return NotFound();

            var genre = await libraryContext.Genres
                                           .Include(g => g.Books)
                                           .FirstOrDefaultAsync(g => g.Id == id);
            if (genre is null)
                return NotFound();

            return View(genre);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Genres";

            if (id is null) return NotFound();

            var genre = await libraryContext.Genres.FindAsync(id);

            if (genre is null) return NotFound();

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Title")] Genre genre)
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Genres";

            if (id != genre.Id) return NotFound();

            if(ModelState.IsValid)
            {
                try
                {
                    libraryContext.Update(genre);
                    await libraryContext.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    if (!GenreExists(genre.Id))
                        NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Genres";

            if(id is null) return NotFound();

            var genre = await libraryContext.Genres
                                           .Include(g => g.Books)
                                           .FirstOrDefaultAsync(g => g.Id == id);

            if(genre is null) return NotFound();

            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            ViewData["MenuItems"] = await dashboardContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Genres";

            var genre = await libraryContext.Genres
                                           .FirstOrDefaultAsync(g => g.Id == id);
            
            if( genre is not null)
                libraryContext.Genres.Remove(genre);

            await libraryContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        private bool GenreExists(int? id)
        {
            return libraryContext.Genres.Any(g => g.Id == id);
        }
    
    }
}
