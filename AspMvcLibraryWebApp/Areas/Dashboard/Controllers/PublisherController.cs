using AspMvcLibraryWebApp.Areas.Dashboard.Models;
using AspMvcLibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspMvcLibraryWebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class PublisherController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;
        private readonly DashboardDbContext dashboardDbContext;

        string imagesDirecory = @$"{Directory.GetCurrentDirectory()}\wwwroot\images";

        public PublisherController()
        {
            libraryDbContext = new LibraryDbContext();
            dashboardDbContext = new DashboardDbContext();
        }

        public async Task<IActionResult> Index()
        {
            ViewData["MenuItems"] = await dashboardDbContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Publishers";

            return View(await libraryDbContext.Publishers.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewData["MenuItems"] = await dashboardDbContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Publishers";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Logo")] Publisher publisher)
        {
            ViewData["MenuItems"] = await dashboardDbContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Publishers";

            if(ModelState.IsValid)
            {
                if(Request.Form.Files.Count > 0)
                {
                    var logo = Request.Form.Files[0];

                    using FileStream file = new(@$"{imagesDirecory}\{logo.FileName}", FileMode.Create);
                    await logo.CopyToAsync(file);

                    publisher.Logo = logo.FileName;
                }

                libraryDbContext.Add(publisher);
                await libraryDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(publisher);
        }
    
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["MenuItems"] = await dashboardDbContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Publishers";

            if (id is null)
                return NotFound();

            var publisher = await libraryDbContext.Publishers
                                                .FindAsync(id);

            if (publisher is null)
                return NotFound();

            return View(publisher);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["MenuItems"] = await dashboardDbContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Publishers";

            if(id is null) return NotFound();

            var publisher = await libraryDbContext.Publishers.FindAsync(id);

            if(publisher is null) return NotFound();

            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Logo")] Publisher publisher)
        {
            ViewData["MenuItems"] = await dashboardDbContext.MainMenuItems.ToListAsync();
            ViewData["Title"] = "Publishers";

            if(id != publisher.Id) return NotFound();

            if(ModelState.IsValid)
            {
                try
                {
                    if (Request.Form.Count > 0)
                    {
                        var logo = Request.Form.Files[0];

                        using FileStream file = new(@$"{imagesDirecory}\{logo.FileName}", FileMode.Create);
                        await logo.CopyToAsync(file);

                        publisher.Logo = logo.FileName;
                    }
                    libraryDbContext.Update(publisher);
                    await libraryDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExists(publisher.Id))
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

            return View(publisher);
        }
    
        private bool PublisherExists(int? id)
        {
            return libraryDbContext.Publishers.Any(p => p.Id == id);
        }
    }
}
