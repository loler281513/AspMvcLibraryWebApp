using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspMvcLibraryWebApp.Models.ModelsConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.AddedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(b => b.Author)
                   .WithMany()
                   .HasForeignKey(b => b.AuthorId);

            builder.HasOne(b => b.Genre)
                   .WithMany(g => g.Books)
                   .HasForeignKey(b => b.GenreId);

            builder.HasOne(b => b.Publisher)
                   .WithMany(p => p.Books)
                   .HasForeignKey(b => b.PublisherId);
        }
    }
}
