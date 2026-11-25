using Microsoft.EntityFrameworkCore;

namespace AspMvcLibraryWebApp.Areas.Dashboard.Models
{
    public class DashboardDbContext : DbContext
    {
        public DbSet<MainMenuItem> MainMenuItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                   .SetBasePath(Directory.GetCurrentDirectory())
                                                   .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MainMenuItem>()
                        .HasData(
                            new MainMenuItem { Id = 1, Title = "Genres", Controller = "Genre" },
                            new MainMenuItem { Id = 2, Title = "Publishers", Controller = "Publisher" },
                            new MainMenuItem { Id = 3, Title = "Authors", Controller = "Author" },
                            new MainMenuItem { Id = 4, Title = "Books", Controller = "Book" }
                        );
        }
    }
}
