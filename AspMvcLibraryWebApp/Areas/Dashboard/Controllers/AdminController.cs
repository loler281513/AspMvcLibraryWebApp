using AspMvcLibraryWebApp.Areas.Dashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspMvcLibraryWebApp.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            using (DashboardDbContext dbContext = new())
            {
                ViewData["MenuItems"] = dbContext.MainMenuItems.ToList();
                ViewData["Title"] = "Dashboard";
            }

            return View();
        }
    }
}
